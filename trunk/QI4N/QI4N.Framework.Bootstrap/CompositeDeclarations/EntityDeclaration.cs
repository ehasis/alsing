namespace QI4N.Framework.Bootstrap
{
    using System;

    public interface EntityDeclaration : AbstractCompositeDeclaration<EntityDeclaration,EntityComposite>
    {
    }

    public class EntityDeclarationImpl : AbstractCompositeDeclarationImpl<EntityDeclaration,EntityComposite>, EntityDeclaration
    {
    }
}