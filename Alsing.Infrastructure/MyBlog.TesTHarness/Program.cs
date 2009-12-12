namespace MyBlog.TesTHarness
{
    using System;
    using System.Collections.Generic;

    using Alsing.Messaging;
    using Alsing.Workspace;

    using Domain;
    using Domain.Projections;
    using Domain.Repositories;

    internal class Program
    {
        private static IMessageBus GetMessageBus(IWorkspace ws)
        {
            var messageBus = new MessageBus();

            return messageBus;
        }

        private static IWorkspace GetWorkspace()
        {
            var ctx = new ModelDataContext
                          {
                                  Log = Console.Out
                          };

            return new LinqToSqlWorkspace(ctx);
        }

        private static void Main(string[] args)
        {
            IWorkspace ws = GetWorkspace();

            var postRepository = new PostRepository(ws);
            IMessageBus messageBus = GetMessageBus(ws);

            IList<FlattenedPost> posts = postRepository.FindLastXPosts(10);

            foreach (FlattenedPost flattenedPost in posts)
            {
                Console.WriteLine("{0} {1} {2} {3}", flattenedPost.PostId, flattenedPost.Subject, flattenedPost.Body, flattenedPost.ReplyCount);
            }

            Post post = postRepository.FindById(3);

            post.Edit(post.Subject, "Det var en gång en häst");

            foreach (Comment reply in post.Comments)
            {
                Console.WriteLine("{0} {1}", reply.Id, reply.Body);
            }

            foreach (PostCategoryLink foo in post.CategoryLinks)
            {
                Console.WriteLine(foo.PostCategory);
            }

            post.ReplyTo(messageBus, "Roggan", "a@a.se", null, "Hej, vilken bra post");

            //    post.Publish();

            ws.Commit();

            Console.ReadLine();
        }
    }
}