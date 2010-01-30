//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Alsing.Workspace;

//namespace MyBlog.Domain.Entities
//{
//    public class Posts
//    {
//        private Blog blog;
//        private IWorkspace workspace;

//        public Posts(Blog blog, IWorkspace workspace)
//        {
//            this.blog = blog;
//            this.workspace = workspace;
//        }

//        public Post NewPost()
//        {
//            var post = new Post(blog);
//            workspace.Add(post);

//            return post;
//        }

//        public void Remove(Post post)
//        {
//            workspace.Remove(post);
//        }

//        public int Count()
//        {
//            return workspace
//                .MakeQuery<Post>()
//                .Where(p => p.BlogId == blog.Id)
//                .Count();
//        }
//    }
//}
