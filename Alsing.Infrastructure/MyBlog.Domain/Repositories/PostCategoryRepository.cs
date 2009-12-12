namespace MyBlog.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Alsing.Workspace;

    public class PostCategoryRepository : Repository<PostCategory>
    {
        public PostCategoryRepository(IWorkspace workspace) : base(workspace)
        {
        }

        public void Add(PostCategory postCategory)
        {
            this
                    .workspace
                    .Add(postCategory);
        }

        public IList<PostCategory> FindAll()
        {
            return this
                    .MakeQuery()
                    .ToList();
        }

        public void Remove(PostCategory postCategory)
        {
            this
                    .workspace
                    .Remove(postCategory);
        }
    }
}