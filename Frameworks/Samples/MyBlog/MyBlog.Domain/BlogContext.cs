using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Workspace;
using Alsing.Messaging;
using MyBlog.Domain.Repositories;

namespace MyBlog.Domain.Entities
{
    public class BlogContext : IDisposable
    {
        public BlogContext(IWorkspace workspace, IMessageBus messageBus)
        {
            this.Workspace = workspace;
            this.MessageBus = messageBus;
        }

        public IWorkspace Workspace { get; private set; }
        public IMessageBus MessageBus { get; private set; }

        public void Dispose()
        {
            Workspace.Dispose();
        }

        private PostRepository posts;
        private CategoryRepository categories;

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
