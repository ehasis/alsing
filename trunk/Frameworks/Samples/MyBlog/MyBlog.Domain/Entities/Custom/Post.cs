using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBlog.Domain.DomainEvents;
using Alsing.Messaging;

namespace MyBlog.Domain.Entities
{
    public partial class Post : IDomainEventAware
    {
        public IMessageSink MessageSink { get; set; }

        public void ReplyTo(string userName,string userEmail,string userWebsite,string text)
        {
            var userInfo = new UserInfo(userName, userEmail, userWebsite);
            var comment = new Comment(this, userInfo, text);
            _comments.Add(comment);
        }

        public void Edit(string subject, string body)
        {
            this.Subject = subject;
            this.Body = body;
        }

        public void EnableComments()
        {
            this.CommentsEnabled = true;
        }

        public void Publish()
        {
            this.PublishDate = DateTime.Now;
        }

        public void AssignCategory(Category category)
        {
            _categories.Add(category);
        }
    }
}
