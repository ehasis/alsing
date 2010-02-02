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
            
            var workspace = GetDomainWorkspace(dataContext);
            var persistedEventBus = GetPersistedEventBus();
            var messageBus = GetEarlyEventBus(dataContext, workspace, persistedEventBus);

            var context = new DomainContext(workspace,messageBus);

            return context;
        }

        private static MessageBus GetEarlyEventBus(Entities dataContext, IWorkspace workspace, MessageBus persistedEventBus)
        {
            var messageBus = new MessageBus();
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
            return messageBus;
        }

        private static MessageBus GetPersistedEventBus()
        {
            var persistedEventBus = new MessageBus();

            //application handlers

            persistedEventBus.RegisterHandler<RepliedToPostEvent>(MessageHandlerType.Synchronous, OnRepliedToPost, true);
            persistedEventBus.RegisterHandler<ApprovedCommentEvent>(MessageHandlerType.Synchronous, OnApprovedComment, true);
            persistedEventBus.RegisterHandler<FailedMessage>(MessageHandlerType.Synchronous, OnFailMessage, false);

            //sync handlers

            persistedEventBus.RegisterHandler<RepliedToPostEvent>(MessageHandlerType.Synchronous, e => UpdateComment( e.Comment), true);
            persistedEventBus.RegisterHandler<ApprovedCommentEvent>(MessageHandlerType.Synchronous, e => UpdateComment( e.Comment), true);

            persistedEventBus.RegisterHandler<AssignedCategoryToPostEvent>(MessageHandlerType.Synchronous, e => CreateCategoryLink(e.Post,e.Category), true);

            persistedEventBus.RegisterHandler<EditedPostEvent>(MessageHandlerType.Synchronous, e => UpdatePost(e.Post), true);
            persistedEventBus.RegisterHandler<EnabledCommentsEvent>(MessageHandlerType.Synchronous, e => UpdatePost(e.Post), true);
            persistedEventBus.RegisterHandler<DisabledCommentsEvent>(MessageHandlerType.Synchronous, e => UpdatePost(e.Post), true);
            persistedEventBus.RegisterHandler<PostCreatedEvent>(MessageHandlerType.Synchronous, e => UpdatePost(e.Post), true);
            persistedEventBus.RegisterHandler<PublishedPostEvent>(MessageHandlerType.Synchronous, e => DeletePost(e.Post), true);
            persistedEventBus.RegisterHandler<UnpublishedPostEvent>(MessageHandlerType.Synchronous, e => DeletePost(e.Post), true);

            persistedEventBus.RegisterHandler<PostDeletedEvent>(MessageHandlerType.Synchronous, e => DeletePost(e.Post), true);


            return persistedEventBus;
        }

        private static void DeletePost(Post post)
        {
         
        }

        private static void UpdatePost(Post post)
        {
            
        }

        private static void CreateCategoryLink(Post post, Category category)
        {
            
        }

        private static void UpdateComment(Comment comment)
        {
            
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
