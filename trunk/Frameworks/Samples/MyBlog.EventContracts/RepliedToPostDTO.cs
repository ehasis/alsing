using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.EventContracts
{
    public class RepliedToPostDTO
    {
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserWebsite { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
