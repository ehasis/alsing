namespace QI4N.Framework.Bootstrap
{
    using System;

    public interface TransientDeclaration : AbstractCompositeDeclaration<TransientDeclaration,TransientComposite>
    {
    }

    public class TransientDeclarationImpl : AbstractCompositeDeclarationImpl<TransientDeclaration,TransientComposite>, TransientDeclaration
    {
    }
}