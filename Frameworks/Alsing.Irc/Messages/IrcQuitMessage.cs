using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Net.Messages;

namespace Alsing.Irc.Messages
{
    public class IrcQuitMessage : ConnectionBaseMessage<IrcConnection>
    {
        public string User { get; set; }
        public string UserIdentity { get; set; }
    }
}
