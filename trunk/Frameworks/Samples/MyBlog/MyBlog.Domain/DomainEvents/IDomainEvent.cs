using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;

namespace MyBlog.Domain.Events
{
    public interface IDomainEvent : IMessage
    {
        object Sender { get; }        
    }
}
