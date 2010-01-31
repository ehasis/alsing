using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;

namespace MyBlog.Domain.Events
{
    public static class DomainEvents
    {
        public static void Raise<T>(T @event) where T : class,IMessage
        {
            DomainEventScope.Raise(@event);
        }
    }

    public class DomainEventScope : IDisposable
    {
        [ThreadStatic]
        private static IMessageSink messageSink;

        private static void BeginScope(IMessageSink messageSink)
        {
            DomainEventScope.messageSink = messageSink;
        }

        private static void EndScope()
        {
            DomainEventScope.messageSink = null;
        }

        private static bool IsInScope()
        {
            return messageSink != null;
        }

        public static void Raise<T>(T @event) where T : class,IMessage
        {
            if (IsInScope() == false)
                throw new Exception("There is no active event scope, call BeginNewScope(messageSink) first!");

            messageSink.Send(@event);
        }

        public DomainEventScope(IMessageSink messageSink)
        {
            if (IsInScope())
                throw new Exception("Nested scopes are not supported");

            BeginScope(messageSink);
        }

        public void Dispose()
        {
            EndScope();
        }
    }
}
