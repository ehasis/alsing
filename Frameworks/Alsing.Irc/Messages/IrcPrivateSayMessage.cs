using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alsing.Irc.Messages
{
    public class IrcPrivateSayMessage : IrcSayMessage
    {
        public string ToUser { get; set; }
    }
}
