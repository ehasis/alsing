using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Workspace;
using MyBlog.Reporting.Projections;
using System.IO;

namespace MyBlog.Reporting.Queries
{
    public class PostQueries
    {
        public PostQueries(TextWriter log)
        {
            context.Log = log;
        }
        private Data.ReportingModelDataContext context = new MyBlog.Reporting.Data.ReportingModelDataContext();

        public Projections.DTOPost FindById(int postId)
        {
            return this
                .context
                .Posts
                .Where(p => p.Id == postId)
                .SelectPost()
                .FirstOrDefault();
        }

        public IList<DTOFlattenedPost> FindLastXPosts(int postCount)
        {
            return this
                .context
                .Posts
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
        internal static IQueryable<DTOFlattenedPost> SelectFlattenedPostProjection(this IQueryable<Data.Post> query)
        {
            return query
                    .Select(post =>
                            new DTOFlattenedPost
                            {
                                Body = post.Body,
                                CreatedDate = post.CreationDate,
                                LastModifiedDate = post.LastModifiedDate,
                                PostId = post.Id,
                                PublishDate = post.PublishDate,
                                ReplyCount = post.Comments.Count(),
                                Subject = post.Subject,
                                Categories = post.PostCategoryLinks.Select(l => new DTOCategory
                                {
                                    CategoryId = l.PostCategory.Id,
                                    Name = l.PostCategory.Name,
                                }),
                            }
                    );
        }

        internal static IQueryable<DTOPost> SelectPost(this IQueryable<Data.Post> query)
        {
            return query
               .Select(p => new Projections.DTOPost()
                {
                    Body = p.Body,
                    CreatedDate = p.CreationDate,
                    LastModifiedDate = p.LastModifiedDate,
                    PostId = p.Id,
                    PublishDate = p.PublishDate,
                    Subject = p.Subject,
                    CommentsEnabled = p.CommentsEnabled,
                    Comments = p.Comments
                                .Where(c => c.Approved)
                                .Select(c =>

                                        new DTOComment
                                        {
                                            Body = c.Body,
                                            UserEmail = c.UserEmail,
                                            UserName = c.UserName,
                                            UserWebsite = c.UserWebSite,
                                            CreationDate = c.CreationDate,
                                        }),
                    Categories = p.PostCategoryLinks
                                    .Select(l =>
                                        new DTOCategory
                                {
                                    CategoryId = l.PostCategory.Id,
                                    Name = l.PostCategory.Name,
                                }),
                });
        }
    }
}
