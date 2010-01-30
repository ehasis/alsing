namespace MyBlog.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Alsing.Workspace;
    using MyBlog.Domain.Entities;

    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(IWorkspace workspace) : base(workspace)
        {
        }

        public void Add(Category postCategory)
        {
            this
                    .workspace
                    .Add(postCategory);
        }

        public void Remove(Category postCategory)
        {
            this
                    .workspace
                    .Remove(postCategory);
        }

        public Category FindById(int categoryId)
        {
            return this.MakeQuery()
                .Where(c => c.Id == categoryId)
                .FirstOrDefault();
        }
    }
}