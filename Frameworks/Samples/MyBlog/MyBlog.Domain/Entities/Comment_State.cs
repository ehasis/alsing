using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Domain.Entities
{
    public partial class Comment
    {
        public virtual int Id { get; set; }
        public virtual string Body { get; set; }
        public virtual bool Approved { get; set; }
        public DateTime CreationDate { get; set; }
        public UserInfo UserInfo { get; set; }
        public Post Post { get; set; }
    }
}
