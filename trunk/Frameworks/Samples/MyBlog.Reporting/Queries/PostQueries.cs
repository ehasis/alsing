using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Workspace;
using MyBlog.Reporting.Projections;

namespace MyBlog.Reporting.Queries
{
    public class PostQueries : Repository<Post>
    {
        public PostQueries(IWorkspace workspace)
            : base(workspace)
        {
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
                                Categories = post.PostCategoryLinks.Select(l => new Category
                                {
                                    CategoryId = l.PostCategory.Id,
                                    Name = l.PostCategory.Name,
                                }),
                            }
                    );
        }
    }
}
