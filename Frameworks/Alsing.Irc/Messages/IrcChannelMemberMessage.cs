using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Net.Messages;

namespace Alsing.Irc.Messages
{
    public class IrcChannelMemberMessage : ConnectionBaseMessage<IrcConnection>, IrcChannelMessage
    {
        public string Channel { get; set; }
        public string MemberName { get; set; }
    }
}
