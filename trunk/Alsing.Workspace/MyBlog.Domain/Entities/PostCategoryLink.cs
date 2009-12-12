namespace MyBlog.Domain
{
    using System;

    public class PostCategoryLink
    {
        [Obsolete(Constants.ForLTSOnly, true)]
        public PostCategoryLink()
        {
        }

        public PostCategoryLink(Post post, PostCategory category)
        {
            this.PostId = post.Id;
            this.PostCategory = category;
        }

        public PostCategory PostCategory { get; private set; }

        private int PostCategoryId { get; set; }

        private int PostId { get; set; }
    }
}