using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBlog.Domain.Entities;
using Alsing.Messaging;

namespace MyBlog.Domain.Events
{
    public class PublishedPostEvent : IDomainEvent
    {
        public PublishedPostEvent(Post post)
        {
            this.Post = post;
        }

        public Post Post { get; set; }

        #region IDomainEvent Members

        public object Sender
        {
            get { return Post; }
        }

        #endregion
    }
}
