namespace QI4N.Framework
{
    using System;

    public interface IdentityGenerator
    {
        string generate(Type compositeType);
    }
}