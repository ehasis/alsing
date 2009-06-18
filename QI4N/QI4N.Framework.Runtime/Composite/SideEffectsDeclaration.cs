namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class SideEffectsDeclaration
    {
        public SideEffectsDeclaration(Type type, IEnumerable<object> objects)
        {
        }

        public MethodSideEffectsModel SideEffectsFor(MethodInfo method, Type mixinType)
        {
            var methodSideEffectModels = new List<MethodSideEffectModel>();
            var model = new MethodSideEffectsModel(method, methodSideEffectModels);
            return model;
        }
    }
}