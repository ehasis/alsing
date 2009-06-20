namespace QI4N.Framework.Runtime
{
    using System.Collections.Generic;

    using Bootstrap;

    public class TransientDeclarationImpl : AbstractCompositeDeclarationImpl<TransientDeclaration, TransientComposite>, TransientDeclaration
    {
        public void AddTransients(List<CompositeModel> compositeModels, PropertyDeclarations propertyDecs)
        {
        }
    }
}