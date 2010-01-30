using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Messaging;
using MyBlog.Domain.Events;

namespace MyBlog.Domain.Entities
{
    public partial class Comment
    {
        protected Comment()
        {
        }

        public Comment(Post post, UserInfo userInfo, string text)
        {
            this.Post = post;
            this.UserInfo = userInfo;
            this.Body = text;
            this.CreationDate = DateTime.Now;
        }

        public void Approve()
        {
            this.Approved = true;

            var message = new ApprovedCommentEvent(this);
            DomainEvents.Raise(message);
        }
    }
}
