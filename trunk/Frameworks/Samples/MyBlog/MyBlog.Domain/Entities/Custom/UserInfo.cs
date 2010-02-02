using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Domain.Entities
{
    public partial class UserInfo
    {
        public UserInfo()
        {
        }

        public UserInfo(string userName, string userEmail, string userWebSite)
        {
            this.Name = userName;
            this.Email = userEmail;
            this.Website = userWebSite;
        }
    }
}
