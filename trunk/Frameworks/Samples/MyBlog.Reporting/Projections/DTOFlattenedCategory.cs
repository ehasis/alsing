using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Reporting.Projections
{
    public class DTOFlattenedCategory
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public int PostCount { get; set; }
    }
}
