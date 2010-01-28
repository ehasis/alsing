namespace MyBlog.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Alsing.Workspace;
    using MyBlog.Domain.Entities;

    public class PostCategoryRepository : Repository<Category>
    {
        public PostCategoryRepository(IWorkspace workspace) : base(workspace)
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
    }
}