using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;
using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Events
{
    public class DisabledCommentsEvent : IMessage
    {
        public DisabledCommentsEvent(Post post)
        {
            this.Post = post;
        }

        public Post Post { get; private set; }
    }
}
