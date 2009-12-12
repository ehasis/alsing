namespace MyBlog.Domain
{
    using System;

    using Alsing.Messaging;

    public class Comment
    {
        public Comment(Post post, string userName, string userEmail, string userWebSite, string body)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            if (string.IsNullOrEmpty(body))
            {
                throw new ArgumentNullException("body");
            }

            this.PostId = post.Id;
            this.UserName = userName;
            this.UserEmail = userEmail;
            this.UserWebSite = userWebSite;
            this.CreationDate = DateTime.Now;
            this.Body = body;

            this.Approved = false;
        }

        [Obsolete(Constants.ForLTSOnly, true)]
        public Comment()
        {
        }

        public bool Approved { get; private set; }

        public string Body { get; private set; }

        public DateTime CreationDate { get; private set; }

        public int Id { get; private set; }

        public string UserEmail { get; private set; }

        public string UserName { get; private set; }

        public string UserWebSite { get; private set; }

        private int PostId { get; set; }

        public void Approve(IMessageSink messageBus)
        {
            this.Approved = true;

            var commentApprovedNotification = new CommentApprovedNotification(this);
            messageBus.Send(commentApprovedNotification);
        }
    }
}