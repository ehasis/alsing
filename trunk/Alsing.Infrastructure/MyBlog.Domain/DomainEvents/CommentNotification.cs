namespace MyBlog.Domain
{
    using Alsing.Messaging;

    public class CommentNotification : IMessage
    {
        public CommentNotification(Comment comment)
        {
            this.Comment = comment;
        }

        public Comment Comment { get; private set; }
    }
}