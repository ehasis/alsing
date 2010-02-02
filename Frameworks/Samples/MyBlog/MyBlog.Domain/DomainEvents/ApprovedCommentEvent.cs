namespace MyBlog.Domain.Events
{
    using Alsing.Messaging;
    using MyBlog.Domain.Entities;

    public class ApprovedCommentEvent : IDomainEvent
    {
        public ApprovedCommentEvent(Comment comment)
        {
            this.Comment = comment;
        }

        public Comment Comment { get; private set; }

        #region IDomainEvent Members

        public object Sender
        {
            get { return Comment; }
        }

        #endregion
    }
}