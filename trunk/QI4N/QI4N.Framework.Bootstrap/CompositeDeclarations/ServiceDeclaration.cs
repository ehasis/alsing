namespace QI4N.Framework.Bootstrap
{
    using System;

    public interface ServiceDeclaration : AbstractCompositeDeclaration<ServiceDeclaration,ServiceComposite>
    {
    }

    public class ServiceDeclarationImpl : AbstractCompositeDeclarationImpl<ServiceDeclaration,ServiceComposite>, ServiceDeclaration
    {
    }
}