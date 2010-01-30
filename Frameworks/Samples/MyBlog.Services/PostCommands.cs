using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using MyBlog.Domain.Repositories;
using MyBlog.Domain.Entities;
using MyBlog.Domain;

namespace MyBlog.Commands
{
    public class PostCommands
    {
        private void Apply(Action<BlogContext> action)
        {
            using (TransactionScope scope = new TransactionScope())
            using (var context = Config.GetNewBlogContext())
            {
                action(context);

                context.Workspace.Commit();
                scope.Complete();
            }
        }

        public void EditPost(int postId, string subject, string body)
        {
            Apply(c => c
                .Posts
                .FindById(postId)
                .Edit(subject, body)
                );
        }

        public void ReplyToPost(int postId, string userName, string userEmail, string userWebsite, string comment)
        {
            Apply(c => c
                .Posts
                .FindById(postId)
                .ReplyTo(userName, userEmail, userWebsite, comment)
                );
        }

        public void Publish(int postId)
        {
            Apply(c => c
                .Posts
                .FindById(postId)
                .Publish()
                );
        }

        public void Unpublish(int postId)
        {
            Apply(c => c
                .Posts
                .FindById(postId)
                .Unpublish()
                );
        }

        public void EnableComments(int postId)
        {
            Apply(c => c
                .Posts
                .FindById(postId)
                .EnableComments()
                );
        }

        public void DisableComments(int postId)
        {
            Apply(c => c
                 .Posts
                 .FindById(postId)
                 .DisableComments()
                 );
        }

        public void AssignCategory(int postId, int categoryId)
        {
            Apply(c => c
                 .Posts
                 .FindById(postId)
                 .AssignCategory(c.Categories.FindById(categoryId))
                 );
        }
    }
}
