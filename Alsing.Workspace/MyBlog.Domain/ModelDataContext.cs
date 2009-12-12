namespace MyBlog.Domain
{
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.IO;

    using Properties;

    [Database(Name = "MyBlog")]
    public class ModelDataContext : DataContext
    {
        private static readonly MappingSource mappingSource = new AttributeMappingSource();

        private static readonly XmlMappingSource xmlSource = XmlMappingSource.FromXml(File.ReadAllText(@"c:\dev\Experimental\Alsing.Workspace\MyBlog.Domain\ModelDataMapping.xml"));

        public ModelDataContext() :
                base(Settings.Default.MyBlogConnectionString, xmlSource)
        {
            var ds = new DataLoadOptions();
            ds.LoadWith<Post>(p => p.Comments);
            ds.LoadWith<Post>(p => p.CategoryLinks);
            ds.LoadWith<PostCategoryLink>(o => o.PostCategory);
            //ds.AssociateWith<Customer>(p => p.Orders.Where(o => o.Freight > 20));
            this.LoadOptions = ds;
        }
    }
}