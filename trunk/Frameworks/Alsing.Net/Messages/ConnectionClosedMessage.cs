using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;

namespace Alsing.Net.Messages
{
    public class ConnectionClosedMessage : ConnectionBaseMessage<ConnectionBase>
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public override string ToString()
        {
            return string.Format("Connection Closed - Host {0}, Port {1}", Host, Port);
        }
    }
}
