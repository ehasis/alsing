namespace Alsing.Messaging
{
    using System;
using System.Collections.Generic;

    public interface IMessageBus : IMessageSink 
    {
        void RegisterHandler<T>(MessageHandlerType handlerType, Action<T> messageHandler, bool catchAndSendExceptions);
        Subject<IMessage> MessageSubject { get; }
        IObservable<T> AsObservable<T>() where T : IMessage;
    }
}