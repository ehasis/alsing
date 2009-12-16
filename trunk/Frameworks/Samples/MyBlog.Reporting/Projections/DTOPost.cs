using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.Reporting.Projections
{
    public class DTOPost
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; internal set; }
        public DateTime? LastModifiedDate { get; internal set; }
        public int PostId { get; internal set; }
        public DateTime? PublishDate { get; internal set; }
        public int ReplyCount { get; internal set; }
        public IEnumerable<DTOComment> Comments { get; set; }
        public bool CommentsEnabled { get; set; }
        public IEnumerable<DTOCategory> Categories { get; set; }
    }
}
