namespace MyBlog.Domain.Events
{
    using Alsing.Messaging;
    using MyBlog.Domain.Entities;

    public class RepliedToPostEvent : IMessage
    {
        public RepliedToPostEvent(Post post,Comment comment)
        {
            this.Post = post;
            this.Comment = comment;
        }

        public Post Post { get; set; }
        public Comment Comment { get; private set; }
    }
}