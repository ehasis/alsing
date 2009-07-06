namespace QI4N.Framework
{
    public class IdentityMixin : Identity
    {
        protected string identity;

        public string Identity
        {
            get
            {
                return this.identity;
            }
        }
    }
}