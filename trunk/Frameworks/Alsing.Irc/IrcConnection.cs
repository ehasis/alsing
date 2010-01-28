using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Net;
using Alsing.Messaging;
using Alsing.Net.Messages;
using Alsing.Irc.Messages;

namespace Alsing.Irc
{
    public class IrcConnection : ConnectionBase
    {
        private readonly IrcCommands commands;
        private  IMessageBus messageBus;
        private string nick;

        public string Nick
        {
            get
            {
                return nick;
            }
        }

        public IrcConnection(IMessageBus messageBus)
            : base(messageBus)
        {
            commands = new IrcCommands(this.WriteLine);
            this.messageBus = messageBus;

            var myMessages = messageBus
                .AsObservable<DataReceivedMessage>()
                .Where(m => m.Connection == this); //only handle messages sent by this connection

            AttachParser(messageBus, myMessages);

            myMessages
                .Where(m => Patterns.ActionRegex.IsMatch(m.Data))
                .Do(_ => Console.ForegroundColor = ConsoleColor.Gray)
                .Do(m => Console.WriteLine("*** ActionRegex Message ****"))
                .Subscribe();

            myMessages
                .Where(m => Patterns.CtcpRequestRegex.IsMatch(m.Data))
                .Do(_ => Console.ForegroundColor = ConsoleColor.Gray)
                .Do(m => Console.WriteLine("*** CtcpRequestRegex Message ****"))
                .Subscribe();

            myMessages
                .Where(m => Patterns.InviteRegex.IsMatch(m.Data))
                .Do(_ => Console.ForegroundColor = ConsoleColor.Gray)
                .Do(m => Console.WriteLine("*** InviteRegex Message ****"))
                .Subscribe();

            myMessages
                .Where(m => Patterns.TopicRegex.IsMatch(m.Data))
                .Do(_ => Console.ForegroundColor = ConsoleColor.Gray)
                .Do(m => Console.WriteLine("*** TopicRegex Message ****"))
                .Subscribe();



            myMessages
                .Where(m => Patterns.KickRegex.IsMatch(m.Data))
                .Do(_ => Console.ForegroundColor = ConsoleColor.Gray)
                .Do(m => Console.WriteLine("*** KickRegex Message ****"))
                .Subscribe();

            myMessages
                .Where(m => Patterns.CtcpReplyRegex.IsMatch(m.Data))
                .Do(_ => Console.ForegroundColor = ConsoleColor.Gray)
                .Do(m => Console.WriteLine("*** CtcpReply Message ****"))
                .Subscribe();
        }

        

        private void AttachParser(IMessageBus messageBus, IObservable<DataReceivedMessage> myMessages)
        {
            //send typed ping message
            myMessages
               .Select(m => Patterns.PingRegex.Match(m.Data))
               .Where(f => f.Success)
               .Select(f => new IrcPingMessage { Connection = this, ServerName = f.Groups[1].Value, })
               .Do(m => messageBus.Send(m))
               .Subscribe();

            //send typed notice message
            myMessages
               .Select(m => Patterns.NoticeRegex.Match(m.Data))
               .Where(f => f.Success)
               .Select(f => new IrcNoticeMessage { Connection = this, Message = f.Groups[2].Value, })
               .Do(m => messageBus.Send(m))
               .Subscribe();

            //send typed reply message
            myMessages
               .Select(m => Patterns.ReplyCodeRegex.Match(m.Data))
               .Where(f => f.Success)
               .Select(f => new IrcReplyCodeMessage { Connection = this, Message = f.Groups[2].Value,Code=int.Parse(f.Groups[1].Value) })
               .Do(m => messageBus.Send(m))
               .Subscribe();

            //send typed error message
            myMessages
               .Select(m => Patterns.ErrorRegex.Match(m.Data))
               .Where(f => f.Success)
               .Select(f => new IrcErrorMessage { Connection = this, Message = f.Value, })
               .Do(m => messageBus.Send(m))
               .Subscribe();

            //send typed Mode message
            myMessages
               .Select(m => Patterns.ModeRegex.Match(m.Data))
               .Where(f => f.Success)
               .Select(f => new IrcModeMessage { Connection = this, Message = f.Value, })
               .Do(m => messageBus.Send(m))
               .Subscribe();
            
            //send typed join message
            myMessages
               .Select(m => Patterns.JoinRegex.Match(m.Data))
               .Where(f => f.Success)
               .Select(f => new IrcJoinMessage 
               { 
                   Connection = this, 
                   User = f.Groups[1].Value,
                   UserIdentity = f.Groups[2].Value,
                   Channel = f.Groups[3].Value.ToUpperInvariant(),
               })
               .Do(m => messageBus.Send(m))
               .Subscribe();

            //send typed part message
            myMessages
               .Select(m => Patterns.PartRegex.Match(m.Data))
               .Where(f => f.Success)
               .Select(f => new IrcPartMessage
               {
                   Connection = this,
                   User = f.Groups[1].Value,
                   UserIdentity = f.Groups[2].Value,
                   Channel = f.Groups[3].Value.ToUpperInvariant()
               })
               .Do(m => messageBus.Send(m))
               .Subscribe();

            //send typed part message
            myMessages
               .Select(m => Patterns.QuitRegex.Match(m.Data))
               .Where(f => f.Success)
               .Select(f => new IrcQuitMessage
               {
                   Connection = this,
                   User = f.Groups[1].Value,
                   UserIdentity = f.Groups[2].Value,
               })
               .Do(m => messageBus.Send(m))
               .Subscribe();

            //send typed channel message
            myMessages
               .Select(m => Patterns.MessageRegex.Match(m.Data))
               .Where(f => f.Success)
               .Where(f => f.Groups[3].Value.StartsWith("#"))
               .Select(f => new IrcChannelSayMessage 
               { 
                   Connection = this,
                   User = f.Groups[1].Value,
                   UserIdentity=f.Groups[2].Value,
                   Channel = f.Groups[3].Value.ToUpperInvariant(),
                   Message=f.Groups[4].Value }
               )
               .Do(m => messageBus.Send(m))
               .Subscribe();

            //send typed private message
            myMessages
               .Select(m => Patterns.MessageRegex.Match(m.Data))
               .Where(f => f.Success)
               .Where(f => !f.Groups[3].Value.StartsWith("#"))
               .Select(f => new IrcPrivateSayMessage
               {
                   Connection = this,
                   User = f.Groups[1].Value,
                   UserIdentity = f.Groups[2].Value,
                   ToUser = f.Groups[3].Value,
                   Message = f.Groups[4].Value
               }
               )
               .Do(m => messageBus.Send(m))
               .Subscribe();

            myMessages
               .Select(m => Patterns.NickRegex.Match(m.Data))
               .Where(f => f.Success)
               .Select(f => new IrcNickMessage
               {
                   Connection = this,
                   User = f.Groups[1].Value,
                   UserIdentity = f.Groups[2].Value,
                   NewNick = f.Groups[3].Value,
               })
               .Do(m => messageBus.Send(m))
               .Subscribe();

            var replyCodes = messageBus.AsObservable<IrcReplyCodeMessage>();

            //send typed channel info
            replyCodes
                .Where(m => m.Code == 332)
                .Select(m => SendChannelInfo(m))
                .Do(m => messageBus.Send(m))
                .Subscribe();

            //send typed channel member info
            replyCodes
                .Where(m => m.Code == 353)                               
                .Do(m => SendChannelMembers(m))
                .Subscribe();

        }

        private IrcChannelInfoMessage SendChannelInfo(IrcReplyCodeMessage m)
        {
            var messageParts = m.Message.Split(' ');
            var channelName = messageParts[1].ToUpperInvariant();
            var title = string.Join(" ",messageParts.Skip(2).ToArray());
            return new IrcChannelInfoMessage { Connection = this, Channel =channelName,Subject=title };
        }

        private void SendChannelMembers(IrcReplyCodeMessage m)
        {
            var messageParts = m.Message.Split(' ');
            var channelName = messageParts[2].ToUpperInvariant();
            var memberNames = messageParts
                                    .Skip(3)
                                    .Where(memberName => !string.IsNullOrEmpty(memberName));

            foreach (var memberName in memberNames)
            {
                string name = memberName;
                if (name.StartsWith(":"))
                    continue;

                var message = new IrcChannelMemberMessage
                {
                    Connection = this,
                    MemberName = name,
                    Channel = channelName,
                };

                messageBus.Send(message);
            }
        }

        public void Logon()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Logging on....");
            Login("KubertYUI", "KOF Re", 1, "Yoda", "Yodar");
        }

        public void Login(string nick, string realname, int usermode, string username, string password)
        {
            this.nick = nick;
            commands.RfcNick(nick);
            commands.RfcUser(username, usermode, realname);
        }

        public IrcCommands Commands
        {
            get
            {
                return commands;
            }
        }
    }
}
