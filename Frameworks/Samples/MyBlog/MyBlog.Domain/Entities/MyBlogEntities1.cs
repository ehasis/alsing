using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.EntityClient;
using System.Data.Objects;

namespace MyBlog.Domain.Entities
{
    public class MyBlogEntities1 : ObjectContext 
    {
        public MyBlogEntities1(EntityConnection conn) : base(conn){ }

        public ObjectSet<Post> Posts
        {
            get
            {
                return base.CreateObjectSet<Post>();
            }
        }

        public ObjectSet<Comment> Comments
        {
            get
            {
                return base.CreateObjectSet<Comment>();
            }
        }

        public ObjectSet<Category> Categories
        {
            get
            {
                return base.CreateObjectSet<Category>();
            }
        }
    }
}
