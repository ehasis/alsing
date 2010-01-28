using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Domain.Entities
{
    public partial class Post
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime LastModifiedDate { get; set; }
        public virtual DateTime? PublishDate { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
        public virtual bool CommentsEnabled { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Category> Categories { get; set; }
    }
}
