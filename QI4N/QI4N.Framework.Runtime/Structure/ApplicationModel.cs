namespace QI4N.Framework.Runtime
{
    using System;

    using Bootstrap;

    public class ApplicationModel
    {
        public static ApplicationModel NewModel(ApplicationAssembly application)
        {
            return new ApplicationModel();
        }

        public ApplicationInstance NewInstance()
        {
            return new ApplicationInstance(this);
        }
    }
}