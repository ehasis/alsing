using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Domain.Entities
{
    public partial class UserInfo
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserWebsite { get; set; }
    }
}
