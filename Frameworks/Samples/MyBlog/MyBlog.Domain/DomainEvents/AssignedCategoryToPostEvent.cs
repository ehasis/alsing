using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;
using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Events
{
    public class AssignedCategoryToPostEvent : IDomainEvent
    {
        public AssignedCategoryToPostEvent(Post post,Category category)
        {
            this.Post = post;
            this.Category = category;
        }

        public Post Post { get;private set; }

        public Category Category { get;private set; }

        #region IDomainEvent Members

        public object Sender
        {
            get { return Post; }
        }

        #endregion
    }
}
