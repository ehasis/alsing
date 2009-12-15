using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Workspace;
using MyBlog.Reporting.Projections;

namespace MyBlog.Reporting.Queries
{
    public class CategoryQueries : Repository<PostCategory>
    {
        public CategoryQueries(IWorkspace workspace)
            : base(workspace)
        {
        }

        public IList<FlattenedCategory> FindAll()
        {
            return this.MakeQuery()
                .SelectFlattenedCategoryProjection()
                .ToList();
        }
    }

    internal static class CategoryExtensions
    {
        /// <summary>
        /// Performs a Entity to Projection transformation using the query context (e.g. inside the database)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        internal static IQueryable<FlattenedCategory> SelectFlattenedCategoryProjection(this IQueryable<PostCategory> query)
        {
            return query
                    .Select(c =>
                            new FlattenedCategory
                            {
                                CategoryId = c.Id,
                                Name = c.Name,
                                PostCount = c.PostCategoryLinks.Where(l => l.Post.PublishDate != null).Count(),
                            }
                    );
        }
    }
}
