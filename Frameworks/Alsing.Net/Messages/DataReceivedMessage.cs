using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;

namespace Alsing.Net.Messages
{
    public class DataReceivedMessage : ConnectionBaseMessage<ConnectionBase>
    {
        public string Data { get; set; }

        public override string ToString()
        {
            return string.Format("Data received - {0}", Data);
        }
    }
}
