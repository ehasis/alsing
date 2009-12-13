using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyBlog.Domain.Repositories;
using Alsing.Workspace;

namespace MyBlog.WebSite
{
    public partial class ShowLatestPosts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (IWorkspace ws = Config.GetWorkspace())
            {
                PostRepository postRepository = new PostRepository(ws);
                var posts = postRepository.FindLastXPosts(10);
                repLatestPosts.DataSource = posts;
                repLatestPosts.DataBind();
            }
        }
    }
}
