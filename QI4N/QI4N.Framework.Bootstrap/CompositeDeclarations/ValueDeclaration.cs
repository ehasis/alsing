namespace QI4N.Framework.Bootstrap
{
    using System;

    public interface ValueDeclaration : AbstractCompositeDeclaration<ValueDeclaration>
    {
    }

    public class ValueDeclarationImpl : AbstractCompositeDeclarationImpl<ValueDeclaration>, ValueDeclaration
    {
        public ValueDeclarationImpl(Type[] types)
            : base(types)
        {
        }
    }
}