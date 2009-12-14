namespace MyBlog.Domain
{
    using Alsing.Messaging;

    public class CommentApproved : IMessage
    {
        public CommentApproved(Comment comment)
        {
            this.Comment = comment;
        }

        public Comment Comment { get; private set; }
    }
}