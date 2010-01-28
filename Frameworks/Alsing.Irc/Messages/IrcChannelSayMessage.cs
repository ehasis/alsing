using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alsing.Irc.Messages
{
    public class IrcChannelSayMessage : IrcSayMessage, IrcChannelMessage
    {
        public string Channel { get; set; }
    }
}
