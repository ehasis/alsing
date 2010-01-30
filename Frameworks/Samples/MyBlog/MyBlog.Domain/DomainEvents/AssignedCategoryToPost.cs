using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;
using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Events
{
    public class AssignedCategoryToPost : IMessage
    {
        public AssignedCategoryToPost(Post post,Category category)
        {
            this.Post = post;
            this.Category = category;
        }

        public Post Post { get;private set; }

        public Category Category { get;private set; }
    }
}
