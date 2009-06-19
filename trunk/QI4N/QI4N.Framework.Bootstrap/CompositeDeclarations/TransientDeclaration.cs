namespace QI4N.Framework.Bootstrap
{
    using System;

    public interface TransientDeclaration : AbstractCompositeDeclaration<TransientDeclaration>
    {
    }

    public class TransientDeclarationImpl : AbstractCompositeDeclarationImpl<TransientDeclaration>, TransientDeclaration
    {
        public TransientDeclarationImpl(Type[] types)
            : base(types)
        {
        }
    }
}