using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;

namespace Alsing.Irc.Messages
{
    public interface IrcChannelMessage : IMessage
    {
        string Channel { get; }
        IrcConnection Connection { get; }
    }
}
