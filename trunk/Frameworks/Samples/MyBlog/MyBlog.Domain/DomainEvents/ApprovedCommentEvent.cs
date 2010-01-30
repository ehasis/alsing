namespace MyBlog.Domain.Events
{
    using Alsing.Messaging;
    using MyBlog.Domain.Entities;

    public class ApprovedCommentEvent : IMessage
    {
        public ApprovedCommentEvent(Comment comment)
        {
            this.Comment = comment;
        }

        public Comment Comment { get; private set; }
    }
}