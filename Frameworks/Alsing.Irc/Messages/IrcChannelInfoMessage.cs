using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Net.Messages;

namespace Alsing.Irc.Messages
{
    public class IrcChannelInfoMessage : ConnectionBaseMessage<IrcConnection>, IrcChannelMessage
    {
        public string Subject { get; set; }
        public string Channel { get; set; }
    }
}
