﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyBlog.Domain.Repositories;
using MyBlog.Domain;

namespace MyBlog.WebSite
{
    public partial class ShowPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var ws = Config.GetWorkspace())
            {
                var postRepository = new PostRepository(ws);
                int postId = this.GetCurrentPostId();
                Post post = postRepository.FindById(postId);

                this.BindPost(post);
            }
        }

        private void BindPost(Post post)
        {
            this.litPublishDate.Text = Utils.FormatDate(post.PublishDate.Value);
            this.litSubject.Text = Utils.FormatText(post.Subject);
            this.litBody.Text = Utils.FormatText(post.Body);

            //Up to the page to decide how to order the comments
            this.repReplies.DataSource = post.Comments.OrderBy(c => c.CreationDate);
            this.repReplies.DataBind();
        }



        private int GetCurrentPostId()
        {
            return int.Parse(this.Request["postId"]);
        }
    }
}