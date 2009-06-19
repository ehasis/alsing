namespace QI4N.Framework.Bootstrap
{
    using System;

    public interface ServiceDeclaration : AbstractCompositeDeclaration<ServiceDeclaration>
    {
    }

    public class ServiceDeclarationImpl : AbstractCompositeDeclarationImpl<ServiceDeclaration>, ServiceDeclaration
    {
        public ServiceDeclarationImpl(Type[] types) : base(types)
        {
        }
    }
}