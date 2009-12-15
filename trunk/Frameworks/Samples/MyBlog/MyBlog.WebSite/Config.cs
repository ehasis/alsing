using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alsing.Workspace;
using Alsing.Messaging;
using MyBlog.Domain;
using System.Diagnostics;

namespace MyBlog.WebSite
{
    public static class Config
    {
        public static IWorkspace GetWorkspace()
        {
            var context = new MyBlog.Domain.ModelDataContext();
            return new LinqToSqlWorkspace(context);
        }

        public static IMessageBus GetMessageBus(IWorkspace workspace)
        {
            var messageBus = new MessageBus();

            messageBus.RegisterHandler<FailedMessage>(MessageHandlerType.Synchronous, OnFailMessage, false);
            messageBus.RegisterHandler<CommentCreated>(MessageHandlerType.Asynchronous, OnCommentCreated, true);
            messageBus.RegisterHandler<CommentApproved>(MessageHandlerType.Asynchronous, OnCommentApproved, true);

            return messageBus;
        }

        private static void OnFailMessage(FailedMessage failMessage)
        {
            EventLog eventLog = new EventLog();
            EventLog.WriteEntry("MyBlog", failMessage.MessageFailureException.ToString());
        }


        private static void OnCommentCreated(CommentCreated commentCreated)
        {
            Trace.Write("comment created handled");
            Debug.Write("comment created handled");
        }

        private static void OnCommentApproved(CommentApproved commentApproved)
        {

        }
    }
}
