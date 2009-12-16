using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Reporting.Projections
{
    public class DTOComment
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserWebsite { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
