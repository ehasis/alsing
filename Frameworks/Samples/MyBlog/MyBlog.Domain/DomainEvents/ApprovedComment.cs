namespace MyBlog.Domain.Events
{
    using Alsing.Messaging;
    using MyBlog.Domain.Entities;

    public class ApprovedComment : IMessage
    {
        public ApprovedComment(Comment comment)
        {
            this.Comment = comment;
        }

        public Comment Comment { get; private set; }
    }
}