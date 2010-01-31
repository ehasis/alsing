namespace MyBlog.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Alsing.Workspace;
    using MyBlog.Domain.Entities;
    using MyBlog.Domain.Events;

    public class PostRepository : Repository<Post>
    {
        public PostRepository(IWorkspace workspace)
            : base(workspace)
        {
        }

        public void Add(Post post)
        {
            this
                    .workspace
                    .Add(post);

            DomainEvents.Raise(new PostCreatedEvent(post));
        }

        public Post FindById(int postId)
        {
            return this
                    .MakeQuery()
                    .Where(post => post.Id == postId)
                    .FirstOrDefault();
        }

        public void Remove(Post post)
        {
            this
                    .workspace
                    .Remove(post);

            DomainEvents.Raise(new PostDeletedEvent(post));
        }
    }
}