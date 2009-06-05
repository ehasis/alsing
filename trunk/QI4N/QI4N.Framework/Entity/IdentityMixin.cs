namespace QI4N.Framework
{
    public class IdentityMixin : Identity
    {
        [State]
        private Property<string> identity;

        public Property<string> Identity
        {
            get
            {
                return this.identity;
            }
        }
    }
}