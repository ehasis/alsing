namespace QI4N.Framework
{
    using System;

    public interface ImportedServiceDescriptor
    {
        Type Type { get; }

        Type ServiceImporter { get; }

        string Identity { get; }

        Visibility Visibility { get; }

        MetaInfo MetaInfo { get; }
    }
}