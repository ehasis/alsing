namespace QI4N.Framework.Runtime
{
    using System;

    public class ImportedServiceModel
    {
        public ImportedServiceModel(Type serviceType,
                                    Visibility visibility,
                                    Type serviceImporter,
                                    string identity,
                                    MetaInfo metaInfo,
                                    string moduleName)
        {
            this.Type = serviceType;
            this.Visibility = visibility;
            this.ServiceImporter = serviceImporter;
            this.Identity = identity;
            this.MetaInfo = metaInfo;
            this.ModuleName = moduleName;
        }

        public string Identity { get; private set; }

        public MetaInfo MetaInfo { get; private set; }

        public string ModuleName { get; private set; }

        public Type ServiceImporter { get; private set; }

        public Type Type { get; private set; }

        public Visibility Visibility { get; private set; }


        public bool IsServiceFor(Type type, Visibility visibility)
        {
            throw new NotImplementedException();
        }
    }
}