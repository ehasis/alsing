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
    using Domain.Exceptions;
    using Domain.Projections;
    using Domain.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var postAboutAOP = new Post();
            postAboutAOP.Edit("AOP for dummies", "...");
            ws.ClearUoW();

            var postRepository = new PostRepository(ws);

            postRepository.Add(postAboutAOP);

            //should be one inserted item
            Assert.AreEqual(1, ws.GetAddedEntityCount<Post>());
        }

        [TestMethod]
        public void Can_approve_comment()
        {
            var ws = new InMemWorkspace();
            IMessageBus messageBus = GetMessageBus(ws);

            var post = new Post();
            post.Edit("AOP for dummies", "...");
            post.Publish();
            post.EnableComments();

            Assert.AreEqual(0, post.Comments.Count());

            post.ReplyTo(messageBus, "Roger Alsing", "roger.alsing@precio.se", "http://www.rogeralsing.com", "Hi there");

            Comment comment = post.Comments.First();

            comment.Approve(messageBus);

            Assert.IsTrue(comment.Approved);
        }

        [TestMethod]
        public void Can_assign_category_to_post()
        {
            var category = new PostCategory("C#");

            var post = new Post();
            post.AssignCategory(category);

            Assert.AreEqual(1, post.CategoryLinks.Count());
        }

        [TestMethod]
        public void Can_edit_post()
        {
            var post = new Post();
            const string expectedSubject = "AOP for dummies";
            const string expectedBody = "...";

            post.Edit(expectedSubject, expectedBody);

            Assert.AreEqual(expectedSubject, post.Subject);
            Assert.AreEqual(expectedBody, post.Body);
        }

        public void Can_find_post_by_id()
        {
            var ws = new InMemWorkspace();
            var postAboutAOP = new Post();
            postAboutAOP.Edit("AOP for dummies", "...");
            ws.Add(postAboutAOP);

            ws.ClearUoW();

            var postRepository = new PostRepository(ws);

            //auto inc id's are not good for tests... get id "0"
            Post foundPost = postRepository.FindById(0);

            Assert.IsTrue(foundPost != null);
        }

        [TestMethod]
        public void Can_get_all_post_categories()
        {
            var ws = new InMemWorkspace();

            ws.Add(new PostCategory("DDD"));
            ws.Add(new PostCategory("AOP"));
            ws.Add(new PostCategory("C#"));

            ws.ClearUoW();

            var postCategoryRepository = new PostCategoryRepository(ws);

            IList<PostCategory> result = postCategoryRepository.FindAll();

            Assert.AreEqual(3, result.Count);

            //assert on unordered result
            Assert.IsTrue(result.Any(c => c.Name == "DDD"));
            Assert.IsTrue(result.Any(c => c.Name == "AOP"));
            Assert.IsTrue(result.Any(c => c.Name == "C#"));
        }

        [TestMethod]
        public void Can_get_last_x_posts_with_reply_count()
        {
            var ws = new InMemWorkspace();
            IMessageBus messageBus = GetMessageBus(ws);
            var postRepository = new PostRepository(ws);

            var postAboutAOP = new Post();
            postAboutAOP.Edit("AOP for dummies", "...");
            postAboutAOP.Publish(new DateTime(2001, 01, 1));
            ws.Add(postAboutAOP);

            var postAboutCS = new Post();
            postAboutCS.Edit("C# for dummies", "...");
            postAboutCS.Publish(new DateTime(2005, 01, 1));
            ws.Add(postAboutCS);

            var postAboutDDD = new Post();
            postAboutDDD.Edit("DDD for dummies", "...");
            postAboutDDD.Publish(new DateTime(2009, 01, 1)); // highest publishdate, should be index 0 in result
            postAboutDDD.EnableComments();
            postAboutDDD.ReplyTo(messageBus, "Roger Alsing", "roger.alsing@precio.se", "http://www.rogeralsing.com", "Hi there");
            postAboutDDD.ReplyTo(messageBus, "Sam the spam", null, null, "Come play poker at ????.???");
            ws.Add(postAboutDDD);

            ws.ClearUoW();

            IList<FlattenedPost> result = postRepository.FindLastXPosts(2);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual("DDD for dummies", result[0].Subject);
            Assert.AreEqual("C# for dummies", result[1].Subject);
            Assert.AreEqual(2, result[0].ReplyCount);
            Assert.AreEqual(0, result[1].ReplyCount);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), Constants.ExceptionCommentsAreNotEnabled)]
        public void Can_not_reply_to_post_when_comments_are_disabled()
        {
            var ws = new InMemWorkspace();
            IMessageBus messageBus = GetMessageBus(ws);

            var post = new Post();
            post.Edit("AOP for dummies", "...");
            post.Publish();

            Assert.AreEqual(0, post.Comments.Count());

            post.ReplyTo(messageBus, "Roger Alsing", "roger.alsing@precio.se", "http://www.rogeralsing.com", "Boom boom pow");
        }

        [TestMethod]
        public void Can_publish_approved_comment_notifications()
        {
            var ws = new InMemWorkspace();
            IMessageBus messageBus = GetMessageBus(ws);
            int numberOfSentNotifications = 0;
            using (var scope = new TransactionScope())
            {
                //register a handler for CommentApprovedNotification, in this test, increase a local variable to
                //hold the number of sent CommentApprovedNotification
                messageBus.RegisterHandler<CommentApprovedNotification>(MessageHandlerType.Synchronous, commentApprovedNotification => OnTransactionCommitted.Invoke(() => numberOfSentNotifications++), false);

                var post = new Post();
                post.Edit("AOP for dummies", "...");
                post.Publish();
                post.EnableComments();

                Assert.AreEqual(0, post.Comments.Count());

                post.ReplyTo(messageBus, "Roger Alsing", "roger.alsing@precio.se", "http://www.rogeralsing.com", "Hi there");

                Comment comment = post.Comments.First();

                comment.Approve(messageBus);

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
            IMessageBus messageBus = GetMessageBus(ws);
            int numberOfSentNotifications = 0;
            using (var scope = new TransactionScope())
            {
                //register a handler for CommentNotifications, in this test, increase a local variable to
                //hold the number of sent CommentNotifications
                messageBus.RegisterHandler<CommentNotification>(MessageHandlerType.Synchronous, commentNotification => OnTransactionCommitted.Invoke(() => numberOfSentNotifications++), false);

                var post = new Post();
                post.Edit("AOP for dummies", "...");
                post.Publish();
                post.EnableComments();

                //pass the DomainEvent container to the method so we can raise domain events in the current context.
                post.ReplyTo(messageBus, "Roger Alsing", "roger.alsing@precio.se", "http://www.rogeralsing.com", "Hi there");

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

            var post = new Post();
            post.Edit("AOP for dummies", "...");
            post.Publish();

            ws.Add(post);
            ws.ClearUoW();

            var postRepository = new PostRepository(ws);

            Assert.AreEqual(0, ws.GetRemovedEntityCount<Post>());

            post = postRepository.FindById(0);
            postRepository.Remove(post);

            Assert.AreEqual(1, ws.GetRemovedEntityCount<Post>());
        }

        [TestMethod]
        public void Can_reply_to_post_when_comments_are_enabled()
        {
            var ws = new InMemWorkspace();
            IMessageBus messageBus = GetMessageBus(ws);

            var post = new Post();
            post.Edit("AOP for dummies", "...");
            post.Publish();
            post.EnableComments();

            Assert.AreEqual(0, post.Comments.Count());

            post.ReplyTo(messageBus, "Roger Alsing", "roger.alsing@precio.se", "http://www.rogeralsing.com", "Hi there");

            //ensure the comment is added to the post
            Assert.AreEqual(1, post.Comments.Count());
        }

        private static IMessageBus GetMessageBus(IWorkspace ws)
        {
            var messageBus = new MessageBus();

            return messageBus;
        }
    }
}