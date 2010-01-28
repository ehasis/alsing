using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Domain.Entities
{
    public partial class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}
