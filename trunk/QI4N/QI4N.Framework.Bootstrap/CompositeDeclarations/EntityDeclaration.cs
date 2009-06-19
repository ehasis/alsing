namespace QI4N.Framework.Bootstrap
{
    using System;

    public interface EntityDeclaration : AbstractCompositeDeclaration<EntityDeclaration>
    {
    }

    public class EntityDeclarationImpl : AbstractCompositeDeclarationImpl<EntityDeclaration>, EntityDeclaration
    {
        public EntityDeclarationImpl(Type[] types) : base(types)
        {
        }
    }
}