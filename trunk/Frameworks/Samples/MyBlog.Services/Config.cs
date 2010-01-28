using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alsing.Workspace;
using Alsing.Messaging;
using MyBlog.Domain;
using System.Diagnostics;
using System.IO;
using MyBlog.Domain.Events;
using MyBlog.Domain.Entities;
using System.Data.SqlClient;
using System.Data.EntityClient;
using MyBlog.Domain.DomainEvents;

namespace MyBlog.Commands
{
    public static class Config
    {
        public static BlogContext GetContext()
        {
            var context = new Entities();
            var messageBus = GetMessageBus();
            var workspace = GetDomainWorkspace(context);

            context.ObjectMaterialized += (s, e) =>
            {
                var entity = e.Entity as IDomainEventAware;
                if (entity != null)
                    entity.MessageSink = messageBus;
            };

            return new BlogContext(workspace,messageBus);
        }
        

        public static IWorkspace GetDomainWorkspace(Entities context)
        {                     
            Func<Type, object> selector = t =>
                {

                    if (t == typeof(MyBlog.Domain.Entities.Post))
                        return context.Posts;
                    
                    if (t == typeof(MyBlog.Domain.Entities.Comment))
                        return context.Comments;

                    if (t == typeof(MyBlog.Domain.Entities.Category))
                        return context.Categories;

                    return null;
                };

            return new EF4Workspace(context,selector);
        }

        public static TextWriter GetLog()
        {
            return new TraceTextWriter();
        }

        public static IMessageBus GetMessageBus()
        {
            var messageBus = new MessageBus();

            messageBus.RegisterHandler<FailedMessage>(MessageHandlerType.Synchronous, OnFailMessage, false);
            messageBus.RegisterHandler<RepliedToPost>(MessageHandlerType.Asynchronous, OnRepliedToPost, true);
            messageBus.RegisterHandler<ApprovedComment>(MessageHandlerType.Asynchronous, OnApprovedComment, true);

            return messageBus;
        }

        private static void OnFailMessage(FailedMessage failMessage)
        {       
            EventLog.WriteEntry("MyBlog", failMessage.MessageFailureException.ToString());
        }


        private static void OnRepliedToPost(RepliedToPost commentCreated)
        {
            Trace.Write("comment created handled");
            Debug.Write("comment created handled");
        }

        private static void OnApprovedComment(ApprovedComment commentApproved)
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
