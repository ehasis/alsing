namespace Alsing.Infrastructure.Tests
{
    using System;
    using System.Threading;

    using Alsing.Messaging;

    using Messaging;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Can_register_multiple_handlers_for_message_type()
        {
            IMessageBus bus = new MessageBus();
            TestMessage receivedMessage1 = null;
            TestMessage receivedMessage2 = null;

            bus.RegisterHandler<TestMessage>(MessageHandlerType.Synchronous, message => receivedMessage1 = message, false);
            bus.RegisterHandler<TestMessage>(MessageHandlerType.Synchronous, message => receivedMessage2 = message, false);

            var sentMessage = new TestMessage
                                  {
                                          Text = "Hello bus"
                                  };

            bus.Send(sentMessage);

            Assert.AreSame(sentMessage, receivedMessage1);
            Assert.AreSame(sentMessage, receivedMessage2);
        }

        [TestMethod]
        public void Can_send_and_receive_async_message()
        {
            var trigger = new AutoResetEvent(false);
            IMessageBus bus = new MessageBus();
            TestMessage receivedMessage = null;

            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            int handlerThreadId = 0;
            bus.RegisterHandler<TestMessage>(MessageHandlerType.Asynchronous, message =>
                                                                              {
                                                                                  handlerThreadId = Thread.CurrentThread.ManagedThreadId;
                                                                                  receivedMessage = message;
                                                                                  trigger.Set();
                                                                              }, false);

            var sentMessage = new TestMessage
                                  {
                                          Text = "Hello bus"
                                  };

            bus.Send(sentMessage);
            trigger.WaitOne(1000);

            //ensure we got the message
            Assert.AreSame(sentMessage, receivedMessage);

            //ensure we didn't handle it in the main thread
            Assert.AreNotEqual(currentThreadId, handlerThreadId);
        }

        [TestMethod]
        public void Can_send_and_receive_sync_message()
        {
            IMessageBus bus = new MessageBus();
            TestMessage receivedMessage = null;

            bus.RegisterHandler<TestMessage>(MessageHandlerType.Synchronous, message => receivedMessage = message, false);

            var sentMessage = new TestMessage
                                  {
                                          Text = "Hello bus"
                                  };

            bus.Send(sentMessage);

            Assert.AreSame(sentMessage, receivedMessage);
        }

        [TestMethod]
        public void Can_send_FailedMessage_when_messagehandler_throws()
        {
            IMessageBus bus = new MessageBus();

            FailedMessage receivedFailedMessage = null;
            bus.RegisterHandler<TestMessage>(MessageHandlerType.Synchronous, message => { throw new Exception("This should throw"); }, true);
            bus.RegisterHandler<FailedMessage>(MessageHandlerType.Synchronous, message => receivedFailedMessage = message, false);

            var sentMessage = new TestMessage
                                  {
                                          Text = "Hello bus"
                                  };

            bus.Send(sentMessage);

            Assert.IsNotNull(receivedFailedMessage);
        }
    }
}