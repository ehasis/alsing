namespace MyBlog.Domain
{
    using Alsing.Messaging;

    public class CommentCreated : IMessage
    {
        public CommentCreated(Comment comment)
        {
            this.Comment = comment;
        }

        public Comment Comment { get; private set; }
    }
}