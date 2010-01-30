using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Domain.Entities
{
    public partial class Blog
    {
        public void AddCategory(string name)
        {
            var category = new Category(name);
        }
    }
}
