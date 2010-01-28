using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alsing.Messaging;
using Alsing.Net.Messages;
using Alsing.Irc.Messages;
using Alsing.Net;
using System.Threading;

namespace Alsing.Irc.Cleint
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private Dictionary<string, IrcForm> forms = new Dictionary<string, IrcForm>(StringComparer.OrdinalIgnoreCase);

        IEnumerable<IrcForm> FormsWithUser(IrcConnection connection, string user)
        {
            foreach (var entry in forms)
            {
                if (entry.Key.StartsWith(connection.Guid.ToString()))
                {
                    if (entry.Value.ContainsUser(user))
                        yield return entry.Value;
                }
            }
        }

        ChannelForm CreateOrFindChannelForm(IrcChannelMessage message)
        {
            var form = CreateOrFindForm<ChannelForm>(message.Connection, message.Channel);
            if (form.Channel == null)
            {
                form.Channel = message.Channel;
                form.Text = string.Format("{0} ({1}, {2})", message.Channel, message.Connection.Host, message.Connection.Nick);
            }

            return form;
        }

        T CreateOrFindForm<T>(IrcConnection connection, string formName) where T : IrcForm, new()
        {
            string key = string.Format("{0}.{1}", connection.Guid, formName);

            if (!forms.ContainsKey(key))
            {
                T form = new T();
                form.MdiParent = this;
                form.Show();
                forms.Add(key, form);
                form.Connection = connection;
            }

            return (T)forms[key];
        }

        private void Gui(Action action)
        {
            this.Invoke(action);
        }

        private MessageBus bus = new MessageBus();

        private void AddHandler<T>(Action<T> action) where T : IMessage
        {
            bus.AsObservable<T>()
                .Do(m => Gui( () => action(m)))
                .Subscribe();
        }
        private void IrcClientMainForm_Load(object sender, EventArgs e)
        {
            AddHandler<ConnectedMessage>(m =>
            {
                var c = (IrcConnection)m.Connection;
                var form = CreateOrFindForm<ServerForm>(c, "ServerForm");
                form.WriteLine(string.Format("Connected to {0} - port {1}", m.Host, m.Port), Color.Red);
                form.Text = string.Format("{0}", m.Connection.Host);
            });

            AddHandler<ConnectionClosedMessage>(m =>
            {
                var form = CreateOrFindForm<ServerForm>((IrcConnection)m.Connection, "ServerForm");
                form.WriteLine(string.Format("Connection closed to {0} - port {1}", m.Host, m.Port), Color.Red);             
            });

            AddHandler<IrcPingMessage>(m =>
            {
                var form = CreateOrFindForm<ServerForm>(m.Connection, "ServerForm");
                form.WriteLine(string.Format("PING {0}", m.ServerName), Color.Magenta);
                form.WriteLine(string.Format("PONG {0}", m.ServerName), Color.Magenta);
                m.Connection.Commands.RfcPong(m.ServerName);
            });

            AddHandler<IrcNoticeMessage>(m =>
            {
                var form = CreateOrFindForm<ServerForm>(m.Connection, "ServerForm");
                form.WriteLine(string.Format(m.Message), Color.DarkRed);
            });

            bus.AsObservable<IrcNoticeMessage>()
                .Take(1)
                .Do(m => m.Connection.Logon())
                .Subscribe();

            AddHandler<IrcReplyCodeMessage>(m =>
            {
                var form = CreateOrFindForm<ServerForm>(m.Connection, "ServerForm");
                form.WriteLine(string.Format(m.Message), Color.Black);
            });

            AddHandler<IrcErrorMessage>(m =>
            {
                var form = CreateOrFindForm<ServerForm>(m.Connection, "ServerForm");
                form.WriteLine(string.Format(m.Message), Color.Red, Color.Yellow);
            });

            AddHandler<IrcModeMessage>(m =>
            {
                var form = CreateOrFindForm<ServerForm>(m.Connection, "ServerForm");
                form.WriteLine(string.Format(m.Message), Color.Green);
            });

            AddHandler<IrcChannelInfoMessage>(m =>
            {
                var form = CreateOrFindChannelForm(m);
                form.WriteLine(string.Format("Now talking in is {0}", m.Channel), Color.Green);
                form.WriteLine(string.Format("Topic is {0}", m.Subject), Color.Green);
                form.Text = m.Subject;
                form.ClearUserList();
            });

            AddHandler<IrcChannelMemberMessage>(m =>
            {
                var form = CreateOrFindChannelForm(m);
                form.AddUser(m.MemberName);
            });

            AddHandler<IrcChannelSayMessage>(m =>
            {
                var form = CreateOrFindChannelForm(m);
                form.Write(string.Format("<{0}> ", m.User), Color.DarkBlue);
                form.WriteLine(m.Message, Color.Black);
            });

            AddHandler<IrcJoinMessage>(m =>
            {
                var form = CreateOrFindChannelForm(m);
                form.WriteLine(string.Format("{0} has joined {1}", m.User, m.Channel), Color.Green);
                form.AddUser(m.User);
            });

            AddHandler<IrcPartMessage>(m =>
            {
                var form = CreateOrFindChannelForm(m);
                form.WriteLine(string.Format("{0} has left {1}", m.User, m.Channel), Color.Green);
                form.RemoveUser(m.User);
            });

            AddHandler<IrcPrivateSayMessage>(m =>
            {
                var form = CreateOrFindForm<WhisperForm>(m.Connection, m.User);
                form.Text = m.User;
                form.User = m.User;
                form.Write(string.Format("<{0}> ", m.User), Color.DarkBlue);
                form.WriteLine(m.Message, Color.Black);
            });

            AddHandler<DataReceivedMessage>(m =>
            {
                ServerForm form = CreateOrFindForm<ServerForm>((IrcConnection)m.Connection, "ServerDebugForm");
                form.WriteLine(m.Data, Color.Gray);
            });

            AddHandler<DataSentMessage>(m =>
            {
                ServerForm form = CreateOrFindForm<ServerForm>((IrcConnection)m.Connection, "ServerDebugForm");
                form.WriteLine(m.Data, Color.LightBlue);
            });

            AddHandler<IrcQuitMessage>(m =>
            {
                foreach (var form in FormsWithUser(m.Connection, m.User))
                {
                    form.WriteLine(string.Format("{0} has quit", m.User), Color.Red);
                }
            });

            AddHandler<IrcNickMessage>(m =>
            {
                foreach (var form in FormsWithUser(m.Connection, m.User))
                {
                    form.WriteLine(string.Format("{0} changed nick to {1}", m.User, m.NewNick), Color.DarkRed);
                }
            });

            IrcConnection connection = new IrcConnection(bus);

            connection.Connect("krypt.ca.us.dal.net", 6663);
            //connection.Connect("irc.dal.net", 6666);
        }
    }
}
