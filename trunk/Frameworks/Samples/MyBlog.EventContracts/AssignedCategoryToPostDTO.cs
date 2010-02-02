using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.EventContracts
{
    public class AssignedCategoryToPostDTO
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
    }
}
