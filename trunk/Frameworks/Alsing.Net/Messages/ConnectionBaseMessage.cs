using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;

namespace Alsing.Net.Messages
{
    public abstract class ConnectionBaseMessage<T> : IMessage where T : ConnectionBase
    {
        protected ConnectionBaseMessage()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get;private  set; }
        public T Connection { get; set; }
    }
}
