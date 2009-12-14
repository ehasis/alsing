namespace MyBlog.Domain
{
    using System;
    using System.Collections.Generic;

    using Alsing.Messaging;

    using Exceptions;

    public class Post
    {
        // ReSharper disable FieldCanBeMadeReadOnly
        // Can not be readonly since LTS uses reflection to set the values
        private IList<PostCategoryLink> categoryLinks;

        private IList<Comment> comments;

        // ReSharper restore FieldCanBeMadeReadOnly

        public Post()
        {
            this.comments = new List<Comment>();
            this.categoryLinks = new List<PostCategoryLink>();
        }

        public string Body { get; private set; }

        public IEnumerable<PostCategoryLink> CategoryLinks
        {
            get
            {
                return this.categoryLinks;
            }
        }

        public IEnumerable<Comment> Comments
        {
            get
            {
                return this.comments;
            }
        }

        public bool CommentsEnabled { get; set; }

        public DateTime CreationDate { get; private set; }

        public int Id { get; private set; }

        public DateTime LastModifiedDate { get; private set; }

        public DateTime? PublishDate { get; private set; }

        public string Subject { get; private set; }

        public void AssignCategory(PostCategory category)
        {
            var link = new PostCategoryLink(this, category);

            this.categoryLinks.Add(link);
        }

        public void DisableComments()
        {
            this.EnsureCommentsEnabled();

            this.CommentsEnabled = false;
        }

        public void Edit(string subject, string body)
        {
            this.Subject = subject;
            this.Body = body;
            this.LastModifiedDate = DateTime.Now;
        }

        public void EnableComments()
        {
            this.EnsureCommentsDisabled();

            this.CommentsEnabled = true;
        }

        public void Publish()
        {
            this.Publish(DateTime.Now);
        }

        public void Publish(DateTime when)
        {
            this.EnsureNotPublished();

            this.PublishDate = when;
        }

        public void ReplyTo(IMessageSink messageSink, string userName, string userEmail, string userWebsite, string body)
        {
            this.EnsurePublished();
            this.EnsureCommentsEnabled();

            var comment = new Comment(this, userName, userEmail, userWebsite, body);
            this.comments.Add(comment);

            var commentCreated = new CommentCreated(comment);
            messageSink.Send(commentCreated);
        }

        public void Unpublish()
        {
            this.EnsurePublished();

            this.PublishDate = null;
        }

        private void EnsureCommentsDisabled()
        {
            if (this.CommentsEnabled)
            {
                throw new DomainException(Constants.ExceptionCommentsAreEnabled);
            }
        }

        private void EnsureCommentsEnabled()
        {
            if (!this.CommentsEnabled)
            {
                throw new DomainException(Constants.ExceptionCommentsAreNotEnabled);
            }
        }

        private void EnsureNotPublished()
        {
            if (this.PublishDate != null)
            {
                throw new DomainException(Constants.ExceptionPostIsPublished);
            }
        }

        private void EnsurePublished()
        {
            if (this.PublishDate == null)
            {
                throw new DomainException(Constants.ExceptionPostIsNotPublished);
            }
        }
    }
}