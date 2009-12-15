using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Domain.Projections
{
    public class FlattenedCategory
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public int PostCount { get; set; }
    }
}
