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
        //    context.Log = log;
        }
        private Data.MyBlogEntities context = new MyBlog.Reporting.Data.MyBlogEntities();

        public IList<DTOFlattenedCategory> FindAll()
        {
            return this.context.Categories
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
        internal static IQueryable<DTOFlattenedCategory> SelectFlattenedCategoryProjection(this IQueryable<Category> query)
        {
            return query
                    .Select(c =>
                            new DTOFlattenedCategory
                            {
                                CategoryId = c.Id,
                                Name = c.Name,
                                PostCount = c.Posts.Where(p => p.PublishDate != null).Count(),
                            }
                    );
        }
    }
}
