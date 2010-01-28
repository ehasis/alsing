using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;

namespace Alsing.Net.Messages
{
    public class ConnectionFailedMessage : ConnectionBaseMessage<ConnectionBase>
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public Exception Exception { get; set; }

        public override string ToString()
        {
            return string.Format("Connection Failed - Host {0}, Port {1}, Exception {2}", Host, Port, Exception);
        }
    }
}
