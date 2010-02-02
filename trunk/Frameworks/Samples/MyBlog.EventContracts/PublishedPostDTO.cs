using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.EventContracts
{
    public class PublishedPostDTO
    {
        public int PostId { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
