namespace MyBlog.Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Transactions;

    using Alsing.Messaging;
    using Alsing.Transactional;
    using Alsing.Workspace;

    using Domain;
    using Domain.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyBlog.Domain.Entities;
    using MyBlog.Domain.Events;

    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class Tests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Can_add_new_post()
        {
           
            var ws = new InMemWorkspace();
            using (var context = GetDomainContext(ws))
            {
                var blog = GetDefaultBlog();
                var postAboutAOP = new Post(blog);
                postAboutAOP.Edit("AOP for dummies", "...");
                ws.ClearUoW();

                context.Posts.Add(postAboutAOP);

                //should be one inserted item
                Assert.AreEqual(1, ws.GetAddedEntityCount<Post>());
            }
        }

        [TestMethod]
        public void Can_approve_comment()
        {
            var ws = new InMemWorkspace();
            using (var context = GetDomainContext(ws))
            {
                var blog = GetDefaultBlog();
                var post = new Post(blog);
                post.Edit("AOP for dummies", "...");
                post.Publish();
                post.EnableComments();
                Assert.AreEqual(0, post.Comments.Count());

                post.ReplyTo("Roger Alsing", "roger.alsing@precio.se", "http://www.rogeralsing.com", "Hi there");

                Comment comment = post.Comments.First();
                comment.Approve();

                Assert.IsTrue(comment.Approved);
            }
        }

        [TestMethod]
        public void Can_assign_category_to_post()
        {
            var ws = new InMemWorkspace();
            using (var context = GetDomainContext(ws))
            {
                var blog = GetDefaultBlog();
                var category = new Category(blog, "C#");
                var post = new Post(blog);
                post.AssignCategory(category);

                Assert.AreEqual(1, post.Categories.Count());
            }
        }

        [TestMethod]
        public void Can_edit_post()
        {
            var ws = new InMemWorkspace();
            using (var context = GetDomainContext(ws))
            {
                var blog = GetDefaultBlog();
                var post = new Post(blog);
                const string expectedSubject = "AOP for dummies";
                const string expectedBody = "...";

                post.Edit(expectedSubject, expectedBody);

                Assert.AreEqual(expectedSubject, post.Subject);
                Assert.AreEqual(expectedBody, post.Body);
            }
        }

        public void Can_find_post_by_id()
        {
            var ws = new InMemWorkspace();
            using (var context = GetDomainContext(ws))
            {
                var blog = GetDefaultBlog();
                var postAboutAOP = new Post(blog);
                postAboutAOP.Edit("AOP for dummies", "...");
                ws.Add(postAboutAOP);

                ws.ClearUoW();

                //auto inc id's are not good for tests... get id "0"
                Post foundPost = context.Posts.FindById(0);

                Assert.IsTrue(foundPost != null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), Constants.ExceptionCommentsAreNotEnabled)]
        public void Can_not_reply_to_post_when_comments_are_disabled()
        {
            var ws = new InMemWorkspace();
            using (var context = GetDomainContext(ws))
            {
                var blog = GetDefaultBlog();
                var post = new Post(blog);
                post.Edit("AOP for dummies", "...");
                post.Publish();

                Assert.AreEqual(0, post.Comments.Count());
                post.ReplyTo("Roger Alsing", "roger.alsing@precio.se", "http://www.rogeralsing.com", "Boom boom pow");
            }
        }

        [TestMethod]
        public void Can_publish_approved_comment_notifications()
        {
            var ws = new InMemWorkspace();            
            int numberOfSentNotifications = 0;

            using (var context = GetDomainContext(ws))
            using (var scope = new TransactionScope())
            {
                //register a handler for CommentApprovedNotification, in this test, increase a local variable to
                //hold the number of sent CommentApprovedNotification
                context.MessageBus.RegisterHandler<ApprovedCommentEvent>(MessageHandlerType.Synchronous, commentApproved => OnTransactionCommitted.Invoke(() => numberOfSentNotifications++), false);

                var blog = GetDefaultBlog();
                var post = new Post(blog);
                post.Edit("AOP for dummies", "...");
                post.Publish();
                post.EnableComments();
                Assert.AreEqual(0, post.Comments.Count());

                post.ReplyTo( "Roger Alsing", "roger.alsing@precio.se", "http://www.rogeralsing.com", "Hi there");

                Comment comment = post.Comments.First();

                comment.Approve();

                //ensure that no comment approved notifications have been processed yet
                Assert.AreEqual(0, numberOfSentNotifications);

                ws.Commit();
                scope.Complete();
            }

            //ensure that one comment approved notification have been processed
            Assert.AreEqual(1, numberOfSentNotifications);
        }


        [TestMethod]
        public void Can_publish_comment_notifications()
        {
            var ws = new InMemWorkspace();
            int numberOfSentNotifications = 0;
            using (var context = GetDomainContext(ws))
            using (var scope = new TransactionScope())
            {
                //register a handler for CommentNotifications, in this test, increase a local variable to
                //hold the number of sent CommentNotifications
                context.MessageBus.RegisterHandler<RepliedToPostEvent>(MessageHandlerType.Synchronous, commentCreated => OnTransactionCommitted.Invoke(() => numberOfSentNotifications++), false);

                var blog = GetDefaultBlog();
                var post = new Post(blog);
                post.Edit("AOP for dummies", "...");
                post.Publish();
                post.EnableComments();
                //pass the DomainEvent container to the method so we can raise domain events in the current context.
                post.ReplyTo("Roger Alsing", "roger.alsing@precio.se", "http://www.rogeralsing.com", "Hi there");

                //ensure that no comment notifications have been processed yet
                Assert.AreEqual(0, numberOfSentNotifications);

                ws.Commit();
                scope.Complete();
            }

            //ensure that one comment notification have been processed
            Assert.AreEqual(1, numberOfSentNotifications);

        }

        [TestMethod]
        public void Can_remove_post()
        {
            var ws = new InMemWorkspace();
            using(var context = GetDomainContext(ws))
            {
                var blog = GetDefaultBlog();
                var post = new Post(blog);
                post.Edit("AOP for dummies", "...");
                post.Publish();

                ws.Add(post);
                ws.ClearUoW();

                Assert.AreEqual(0, ws.GetRemovedEntityCount<Post>());

                post = context.Posts.FindById(0);
                context.Posts.Remove(post);

                Assert.AreEqual(1, ws.GetRemovedEntityCount<Post>());
            }
        }

        [TestMethod]
        public void Can_reply_to_post_when_comments_are_enabled()
        {
            var ws = new InMemWorkspace();
            using (var context = GetDomainContext(ws))
            {
                var blog = GetDefaultBlog();
                var post = new Post(blog);
                post.Edit("AOP for dummies", "...");
                post.Publish();
                post.EnableComments();

                Assert.AreEqual(0, post.Comments.Count());

                post.ReplyTo( "Roger Alsing", "roger.alsing@precio.se", "http://www.rogeralsing.com", "Hi there");

                //ensure the comment is added to the post
                Assert.AreEqual(1, post.Comments.Count());
            }
        }

        private Blog GetDefaultBlog()
        {
            return new Blog();
        }

        private static IMessageBus GetMessageBus()
        {
            var messageBus = new MessageBus();
            return messageBus;
        }

        private static DomainContext GetDomainContext(IWorkspace ws)
        {
            var messageBus = GetMessageBus();

            return new DomainContext(ws, messageBus);
        }
    }
}