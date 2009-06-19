namespace QI4N.Framework.Bootstrap
{
    using System;

    public interface ValueDeclaration : AbstractCompositeDeclaration<ValueDeclaration,ValueComposite>
    {
    }

    public class ValueDeclarationImpl : AbstractCompositeDeclarationImpl<ValueDeclaration,ValueComposite>, ValueDeclaration
    {
    }
}