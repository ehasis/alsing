using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Domain.Entities
{
    public partial class Category
    {
        private Category()
        {
        }

        public Category(string name)
        {
            this.Name = name;
        }
    }
}
