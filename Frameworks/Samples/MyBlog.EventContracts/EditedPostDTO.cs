using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.EventContracts
{
    public class EditedPostDTO
    {
        public int PostId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
