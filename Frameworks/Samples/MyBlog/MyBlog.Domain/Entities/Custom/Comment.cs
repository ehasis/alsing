using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBlog.Domain.DomainEvents;
using Alsing.Messaging;

namespace MyBlog.Domain.Entities
{
    public partial class Comment : IDomainEventAware
    {
        public IMessageSink MessageSink { get; set; }

        public Comment()
        {
        }

        public Comment(Post post, UserInfo userInfo, string text)
        {
            // TODO: Complete member initialization
            this.Post = post;
            this.UserInfo = userInfo;
            this.Body = text;
            this.CreationDate = DateTime.Now;
        }

        public void Approve()
        {
            this.Approved = true;
        }
    }
}
