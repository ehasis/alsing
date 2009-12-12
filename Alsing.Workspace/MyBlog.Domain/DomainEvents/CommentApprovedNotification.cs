namespace MyBlog.Domain
{
    using Alsing.Messaging;

    public class CommentApprovedNotification : IMessage
    {
        public CommentApprovedNotification(Comment comment)
        {
            this.Comment = comment;
        }

        public Comment Comment { get; private set; }
    }
}