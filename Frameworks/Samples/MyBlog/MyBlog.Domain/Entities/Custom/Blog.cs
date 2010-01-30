using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Domain.Entities
{
    public partial class Blog
    {
        public Category NewCategory(string name)
        {
            var category = new Category(this,name);
            return category;
        }

        public Post NewPost()
        {
            var post = new Post(this);
            return post;
        }

        public void Edit(string title)
        {
            this.Title = title;
        }
    }
}
