namespace QI4N.Framework
{
    public class ServiceReferenceFacade<T> : ServiceReference<T>
    {
        private readonly ServiceReference instance;

        public ServiceReferenceFacade(ServiceReference instance)
        {
            this.instance = instance;
        }

        public string Identity
        {
            get
            {
                return this.instance.Identity;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.instance.IsActive;
            }
        }

        public MetaInfo MetaInfo
        {
            get
            {
                return this.instance.MetaInfo;
            }
        }

        public T Get()
        {
            return (T)this.instance.Get();
        }

        object ServiceReference.Get()
        {
            return this.Get();
        }
    }
}