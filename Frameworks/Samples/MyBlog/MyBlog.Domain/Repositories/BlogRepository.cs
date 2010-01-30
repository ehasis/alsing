using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Workspace;
using MyBlog.Domain.Entities;

namespace MyBlog.Domain.Repositories
{
    public class BlogRepository : Repository<Blog>
    {
        public BlogRepository(IWorkspace ws)
            : base(ws)
        {
        }

        public void Add(Blog blog)
        {
            this
                .workspace
                .Add(blog);
        }

        public Blog FindById(int blogId)
        {
            return MakeQuery()
                .Where(blog => blog.Id == blogId)
                .FirstOrDefault();
        }

        public void Remove(Blog blog)
        {
            this
                .workspace
                .Remove(blog);
        }
    }
}
