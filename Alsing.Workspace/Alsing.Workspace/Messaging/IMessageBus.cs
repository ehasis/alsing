namespace Alsing.Messaging
{
    using System;

    public interface IMessageBus : IMessageSink
    {
        void RegisterHandler<T>(MessageHandlerType handlerType, Action<T> messageHandler, bool catchAndSendExceptions);
    }
}