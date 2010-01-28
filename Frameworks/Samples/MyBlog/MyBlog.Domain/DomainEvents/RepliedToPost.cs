namespace MyBlog.Domain.Events
{
    using Alsing.Messaging;
    using MyBlog.Domain.Entities;

    public class RepliedToPost : IMessage
    {
        public RepliedToPost(Comment comment)
        {
            this.Comment = comment;
        }

        public Comment Comment { get; private set; }
    }
}