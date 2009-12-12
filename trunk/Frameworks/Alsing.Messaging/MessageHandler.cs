namespace Alsing.Messaging
{
    using System;

    public class MessageHandler<T>
    {
        public Action<T> Handler { get; set; }

        public MessageHandlerType Type { get; set; }
    }
}