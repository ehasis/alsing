using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alsing.Workspace;
using Alsing.Messaging;
using MyBlog.Domain;
using System.Diagnostics;
using System.IO;

namespace MyBlog.Services
{
    public static class Config
    {
        public static IWorkspace GetDomainWorkspace()
        {
            var context = new MyBlog.Domain.ModelDataContext();

            context.Log = GetLog();

            return new LinqToSqlWorkspace(context);
        }

        public static TextWriter GetLog()
        {
            return new TraceTextWriter();
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

    public class TraceTextWriter : System.IO.TextWriter
    {
        public override System.Text.Encoding Encoding
        {
            get { throw new NotImplementedException(); }
        }

        public override void WriteLine(string value)
        {
            HttpContext.Current.Trace.Write("LINQ", value);
            base.WriteLine(value);
        }
    }
}
