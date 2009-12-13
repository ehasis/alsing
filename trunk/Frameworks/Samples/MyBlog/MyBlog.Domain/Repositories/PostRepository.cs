namespace MyBlog.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Alsing.Workspace;

    using Projections;

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
        }

        public IList<FlattenedPost> FindByCategoryId(int categoryId)
        {
            return this
                    .MakeQuery()
                    .OrderByDescending(post => post.PublishDate)
                    .Where(post => post
                                           .CategoryLinks
                                           .Any(l => l.PostCategory.Id == categoryId)
                    )
                    .SelectFlattenedPostProjection()
                    .ToList();
        }

        public Post FindById(int postId)
        {
            return this
                    .MakeQuery()
                    .Where(post => post.Id == postId)
                    .FirstOrDefault();
        }

        public IList<FlattenedPost> FindLastXPosts(int postCount)
        {
            return this
                    .MakeQuery()
                    .OrderByDescending(post => post.PublishDate)
                    .Take(postCount)
                    .SelectFlattenedPostProjection()
                    .ToList();
        }

        public void Remove(Post post)
        {
            this
                    .workspace
                    .Remove(post);
        }
    }

    internal static class PostExtensions
    {
        /// <summary>
        /// Performs a Entity to Projection transformation using the query context (e.g. inside the database)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static IQueryable<FlattenedPost> SelectFlattenedPostProjection(this IQueryable<Post> query)
        {
            return query
                    .Select(post =>
                            new FlattenedPost
                                {
                                        Body = post.Body,
                                        CreatedDate = post.CreationDate,
                                        LastModifiedDate = post.LastModifiedDate,
                                        PostId = post.Id,
                                        PublishDate = post.PublishDate,
                                        ReplyCount = post.Comments.Count(),
                                        Subject = post.Subject,
                                        CategoryLinks = post.CategoryLinks,
                                }
                    );
        }
    }
}