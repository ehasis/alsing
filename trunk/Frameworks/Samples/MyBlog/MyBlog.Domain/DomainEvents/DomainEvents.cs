using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;

namespace MyBlog.Domain.Events
{
    public static class DomainEvents
    {
        [ThreadStatic]
        private static IMessageSink messageSink;

        public static void BeginNewScope(IMessageSink messageSink)
        {
            DomainEvents.messageSink = messageSink;
        }

        public static void Raise<T>(T @event) where T : class,IMessage
        {
            if (messageSink == null)
                throw new Exception("There is no active event scope, call BeginNewScope(messageSink) first!");

            messageSink.Send(@event);
        }
    }
}
