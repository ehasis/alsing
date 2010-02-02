using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.EventContracts
{
    public class ApprovedCommentDTO
    {
        public int PostId { get; set; }
        public int CommentId { get; set; }
    }
}
