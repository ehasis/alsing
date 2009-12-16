using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Alsing.Workspace;
using MyBlog.Domain.Repositories;
using MyBlog.Reporting.Queries;

namespace MyBlog.WebSite
{
    public partial class MyMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            CategoryQueries categoryRepository = new CategoryQueries(Config.GetLog());
            var categories = categoryRepository.FindAll();

            repCategories.DataSource = categories.OrderBy(c => c.Name);
            repCategories.DataBind();

        }
    }
}
