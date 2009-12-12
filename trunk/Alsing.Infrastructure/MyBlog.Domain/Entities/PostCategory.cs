namespace MyBlog.Domain
{
    using System;

    public class PostCategory
    {
        [Obsolete(Constants.ForLTSOnly, true)]
        public PostCategory()
        {
        }

        public PostCategory(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            this.Name = name;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}