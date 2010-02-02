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
using Alsing.Transactional;
using System.Data.Objects;
using MyBlog.Commands.Services;

namespace MyBlog.Commands
{
    public static class Config
    {
        public static DomainContext GetBlogContext()
        {
            var dataContext = new Entities();
            var messageBus = new MessageBus();
            var workspace = GetDomainWorkspace(dataContext);

            var persistedEventBus = new MessageBus();
            
            
            //handler for phase 1
            messageBus
                .AsObservable<IDomainEvent>()
                .Do(e =>
                    workspace.Committing += (s, ea) =>
                    {
                        ObjectStateEntry entry;
                        if (dataContext.ObjectStateManager.TryGetObjectStateEntry(e.Sender, out entry))
                        {
                            //resend the same event after commit
                            persistedEventBus.Send(e);
                        }
                    })
                .Subscribe();

            //handler for phase 2
            persistedEventBus
                .AsObservable<RepliedToPostEvent>()
                .Do(e => OnRepliedToPost(e))
                .Subscribe();

            persistedEventBus
                .AsObservable<ApprovedCommentEvent>()
                .Do(e => OnApprovedComment(e))
                .Subscribe();

            var context = new DomainContext(workspace,messageBus);

            return context;
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

        private static void OnFailMessage(FailedMessage failMessage)
        {       
            EventLog.WriteEntry("MyBlog", failMessage.MessageFailureException.ToString());
        }


        private static void OnRepliedToPost(RepliedToPostEvent e)
        {
            IEmailService emailService = GetEmailService();
            emailService.SendEmail("roger.alsing@precio.se", "New comment for post: " + e.Post.Subject, "Click http://foo.bar/ApproveComment.aspx?commentId=" + e.Comment.Id);

        }

        private static void OnApprovedComment(ApprovedCommentEvent commentApproved)
        {

        }

        private static IEmailService GetEmailService()
        {
            return new DefaultEmailService();
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
