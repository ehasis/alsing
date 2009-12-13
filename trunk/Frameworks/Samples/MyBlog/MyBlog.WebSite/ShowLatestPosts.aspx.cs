using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alsing.Workspace;
using MyBlog.Domain.Repositories;

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
                repLastPosts.DataSource = posts;
                repLastPosts.DataBind();
            }
        }
    }
}
