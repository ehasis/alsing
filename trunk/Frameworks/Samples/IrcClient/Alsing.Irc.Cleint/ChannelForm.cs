using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Alsing.Irc.Cleint
{
    public partial class ChannelForm : IrcForm
    {
        public string Channel { get; set; }

        public ChannelForm()
        {
            InitializeComponent();
        }

        private void ChannelForm_Load(object sender, EventArgs e)
        {

        }

        public void ClearUserList()
        {
            UserList.Items.Clear();
        }

        public void AddUser(string userName)
        {
            UserList.Items.Add(userName);
        }

        public void RemoveUser(string userName)
        {
            if (UserList.Items.Contains(userName) )
                UserList.Items.Remove(userName);
        }

        protected override void OnEnterKey(string text)
        {
            Connection.Commands.RfcPrivmsg(this.Channel, text);
        }

        public override bool ContainsUser(string user)
        {
            var exists = UserList
                .Items
                .Cast<string>()
                .Select(u => u.ToLowerInvariant())
                .Contains(user.ToLowerInvariant());

            return exists;
        }
    }
}
