using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBlog.Domain.Entities;
using Alsing.Messaging;

namespace MyBlog.Domain.Events
{
    public class UnpublishedPost : IMessage
    {
        public UnpublishedPost(Post post)
        {
            this.Post = post;
        }

        public Post Post { get; set; }
    }
}
