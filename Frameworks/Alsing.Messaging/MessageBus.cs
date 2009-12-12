namespace Alsing.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public class MessageBus : IMessageBus
    {
        private readonly Dictionary<Type, IList<object>> handlers = new Dictionary<Type, IList<object>>();

        public void RegisterHandler<T>(MessageHandlerType handlerType, Action<T> messageHandler, bool catchAndSendExceptions)
        {
            if (catchAndSendExceptions)
            {
                messageHandler = this.WrapActionWithErrorHandling(messageHandler);
            }

            if (!this.handlers.ContainsKey(typeof(T)))
            {
                this.handlers.Add(typeof(T), new List<object>());
            }

            IList<object> handlersForType = this.handlers[typeof(T)];

            var handler = new MessageHandler<T>
                              {
                                      Type = handlerType,
                                      Handler = messageHandler,
                              };
            handlersForType.Add(handler);
        }

        public void Send<T>(T message) where T : class, IMessage
        {
            IEnumerable<MessageHandler<T>> handldersForType = this.GetHandlers<T>();
            foreach (var handlerForType in handldersForType)
            {
                switch (handlerForType.Type)
                {
                    case MessageHandlerType.Synchronous:
                        handlerForType.Handler(message);
                        break;
                    case MessageHandlerType.Asynchronous:                    
                        handlerForType.Handler.BeginInvoke(message, null, null);
                        break;
                }
            }
        }

        private IEnumerable<MessageHandler<T>> GetHandlers<T>()
        {
            if (this.handlers.ContainsKey(typeof(T)))
            {
                IList<object> handlersForType = this.handlers[typeof(T)];
                return handlersForType.Cast<MessageHandler<T>>();
            }

            return new List<MessageHandler<T>>();
        }

        private Action<T> WrapActionWithErrorHandling<T>(Action<T> action)
        {
            Action<T> wrappedAction = message =>
                                      {
                                          try
                                          {
                                              action(message);
                                          }
                                          catch (Exception x)
                                          {
                                              var failedMessage = new FailedMessage
                                                                      {
                                                                              MessageFailureException = x
                                                                      };
                                              this.Send(failedMessage);
                                          }
                                      };
            return wrappedAction;
        }
    }
}