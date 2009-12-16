using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyBlog.Domain.Repositories;
using System.Transactions;
using MyBlog.Reporting.Queries;
using MyBlog.Reporting.Projections;
using MyBlog.Domain;
using MyBlog.Services;

namespace MyBlog.WebSite
{
    public partial class ShowPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindPost();
            }
        }

        private void BindPost()
        {
            PostQueries postRepository = new PostQueries(Config.GetLog());
            int postId = this.GetCurrentPostId();
            MyBlog.Reporting.Projections.DTOPost post = postRepository.FindById(postId);

            this.litPublishDate.Text = Utils.FormatDate(post.PublishDate.Value);
            this.litSubject.Text = Utils.FormatText(post.Subject);
            this.litBody.Text = Utils.FormatText(post.Body);
            this.litCommentCount.Text = post.Comments.Count().ToString();
            this.litCategories.Text = FormatCategories(post.Categories);

            //Up to the page to decide how to order the comments
            this.repReplies.DataSource = post.Comments.OrderBy(c => c.CreationDate);
            this.repReplies.DataBind();

            pnlReply.Visible = post.CommentsEnabled;
        }

        private int GetCurrentPostId()
        {
            return int.Parse(this.Request["postId"]);
        }

        //TODO: fix this
        public string FormatCategories(object o)
        {
            IEnumerable<DTOCategory> links = o as IEnumerable<DTOCategory>;

            var strings = links.Select(l => l.Name).ToArray();

            return string.Join(", ", strings);
        }

        protected string FormatCreationDate(object o)
        {
            DateTime dt = (DateTime)o;
            return Utils.FormatDateTime(dt);
        }

        protected void btnSubmitComment_Click(object sender, EventArgs e)
        {
            int postId = this.GetCurrentPostId();
            string userName = txtUserName.Text;
            string userEmail  = txtUserEmail.Text;
            string userWebSite = txtUserWebSite.Text;
            string comment = txtComment.Text;

            ReplyToPost(postId, userName, userEmail, userWebSite, comment);   

            txtComment.Text = "";
        }

        private void ReplyToPost(int postId, string userName, string userEmail, string userWebSite, string comment)
        {
            PostCommands commands = new PostCommands();
            commands.ReplyToPost(postId, userName, userEmail, userWebSite, comment);
            BindPost();
        }
    }
}
