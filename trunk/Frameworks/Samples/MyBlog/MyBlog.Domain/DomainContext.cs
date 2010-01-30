using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Workspace;
using Alsing.Messaging;
using MyBlog.Domain.Repositories;
using MyBlog.Domain.Events;

namespace MyBlog.Domain
{
    public class DomainContext : IDisposable
    {
        public DomainContext(IWorkspace workspace, IMessageBus messageBus)
        {
            this.Workspace = workspace;
            this.MessageBus = messageBus;
            this.DomainEvents = new DomainEventScope(messageBus);
        }

        private DomainEventScope DomainEvents { get; set; }
        public IWorkspace Workspace { get; private set; }
        public IMessageBus MessageBus { get; private set; }

        public void Dispose()
        {
            Workspace.Dispose();
            DomainEvents.Dispose();
        }

        private PostRepository posts;
        private CategoryRepository categories;
        private BlogRepository blogs;

        public BlogRepository Blogs
        {
            get
            {
                if (blogs == null)
                    blogs = new BlogRepository(Workspace);

                return blogs;
            }
        }

        public PostRepository Posts
        {
            get
            {
                if (posts == null)
                    posts = new PostRepository(Workspace);

                return posts;
            }
        }

        public CategoryRepository Categories
        {
            get
            {
                if (categories == null)
                    categories = new CategoryRepository(Workspace);

                return categories;
            }
        }
    }
}
