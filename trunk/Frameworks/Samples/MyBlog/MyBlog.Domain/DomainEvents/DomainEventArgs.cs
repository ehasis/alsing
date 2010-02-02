using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;

namespace MyBlog.Domain.Events
{


    public class DomainEventArgs<TSender, TEvent> : IMessage
    {
        public DomainEventArgs(TSender sender, TEvent @event)
        {
            this.Sender = sender;
            this.Event = @event;
        }
        public TSender Sender { get; private set; }
        public TEvent Event { get; private set; }
    }
}
