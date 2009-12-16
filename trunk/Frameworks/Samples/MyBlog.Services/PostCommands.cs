using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using MyBlog.Domain.Repositories;

namespace MyBlog.Services
{
    public class PostCommands
    {
        public void ReplyToPost(int postId, string userName, string userEmail, string userWebsite, string comment)
        {
            using (TransactionScope scope = new TransactionScope())
            using (var ws = Config.GetDomainWorkspace())
            {
                var messageBus = Config.GetMessageBus(ws);
                var postRepository = new PostRepository(ws);

                MyBlog.Domain.Post post = postRepository.FindById(postId);

                post.ReplyTo(messageBus, userName, userEmail, userWebsite, comment);
                ws.Commit();

                scope.Complete();
            }
        }

        public void Publish(int postId)
        {
        }

        public void Unpublish(int postId)
        {
        }

        public void EnableComments(int postId)
        { 
        }

        public void DisableComments(int postId)
        {
        }

        public void AssignCategory(int postId,int categoryId)
        {
        }
    }
}
