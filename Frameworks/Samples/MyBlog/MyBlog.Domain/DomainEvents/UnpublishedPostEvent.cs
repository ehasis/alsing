using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBlog.Domain.Entities;
using Alsing.Messaging;

namespace MyBlog.Domain.Events
{
    public class UnpublishedPostEvent : IMessage
    {
        public UnpublishedPostEvent(Post post)
        {
            this.Post = post;
        }

        public Post Post { get;private set; }
    }
}
