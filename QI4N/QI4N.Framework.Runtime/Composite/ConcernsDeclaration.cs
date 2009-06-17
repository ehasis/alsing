namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class ConcernsDeclaration
    {
        public ConcernsDeclaration(IList<ConcernDeclaration> concerns)
        {
        }

        public static void ConcernDeclarations(Type mixinType, List<ConcernDeclaration> concerns)
        {
        }

        public static void ConcernDeclarations(IEnumerable<object> concerns, IList<ConcernDeclaration> list)
        {
        }

        public MethodConcernsModel ConcernsFor(MethodInfo method, Type type)
        {
            var methodConcernModels = new List<MethodConcernModel>();
            var model = new MethodConcernsModel(method, methodConcernModels);
            return model;
        }
    }
}