using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Workspace;
using MyBlog.Reporting.Projections;
using MyBlog.Reporting.Data;
using System.IO;

namespace MyBlog.Reporting.Queries
{
    public class CategoryQueries
    {
        public CategoryQueries(TextWriter log)
        {
            context.Log = log;
        }
        private Data.ReportingModelDataContext context = new MyBlog.Reporting.Data.ReportingModelDataContext();

        public IList<DTOFlattenedCategory> FindAll()
        {
            return this.context.PostCategories
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
        internal static IQueryable<DTOFlattenedCategory> SelectFlattenedCategoryProjection(this IQueryable<PostCategory> query)
        {
            return query
                    .Select(c =>
                            new DTOFlattenedCategory
                            {
                                CategoryId = c.Id,
                                Name = c.Name,
                                PostCount = c.PostCategoryLinks.Where(l => l.Post.PublishDate != null).Count(),
                            }
                    );
        }
    }
}
