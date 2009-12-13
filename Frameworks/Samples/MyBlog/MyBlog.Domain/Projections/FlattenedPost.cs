namespace MyBlog.Domain.Projections
{
    using System;
    using System.Collections.Generic;

    //View specific projection of Entity "Post"
    //Flattens reply count into a scalar property instead of list
    //
    //uses poor mans immutabillity
    public class FlattenedPost
    {
        public string Body { get; internal set; }

        public DateTime CreatedDate { get; internal set; }

        public DateTime? LastModifiedDate { get; internal set; }

        public int PostId { get; internal set; }

        public DateTime? PublishDate { get; internal set; }

        public int ReplyCount { get; internal set; }

        public string Subject { get; internal set; }

        public IEnumerable<PostCategoryLink> CategoryLinks { get; set; }
    }
}