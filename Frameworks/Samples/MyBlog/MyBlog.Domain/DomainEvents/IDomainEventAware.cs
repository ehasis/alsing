using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;

namespace MyBlog.Domain.DomainEvents
{
    public interface IDomainEventAware
    {
        IMessageSink MessageSink { get; set; }  
    }
}
