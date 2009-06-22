namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Reflection;

    public class ConcernsDeclaration
    {
        private readonly IList<ConcernDeclaration> concerns;

        private readonly Dictionary<MethodInfo, MethodConcernsModel> methodConcernsModels = new Dictionary<MethodInfo, MethodConcernsModel>();

        public ConcernsDeclaration(IList<ConcernDeclaration> concerns)
        {
            this.concerns = concerns;
        }

        public static void ConcernDeclarations(Type mixinType, List<ConcernDeclaration> concerns)
        {
            // Find concern declarations
            var types = new List<Type>(mixinType.GetAllInterfaces());

            foreach (Type aType in types)
            {
                AddConcernDeclarations(aType, concerns);
            }
        }

        public static void ConcernDeclarations(IEnumerable<Type> concernTypes, IList<ConcernDeclaration> concerns)
        {
            // Add concerns from assembly
            foreach (Type concern in concernTypes)
            {
                concerns.Add(new ConcernDeclaration(concern, null));
            }
        }

        public MethodConcernsModel ConcernsFor(MethodInfo method, Type type)
        {
            if (!this.methodConcernsModels.ContainsKey(method))
            {
                var concernsForMethod = new List<MethodConcernModel>();
                foreach (ConcernDeclaration concern in this.concerns)
                {
                    if (concern.AppliesTo(method, type))
                    {
                        Type concernType = concern.ModifierClass;
                        concernsForMethod.Add(new MethodConcernModel(concernType));
                    }
                }

                var methodConcerns = new MethodConcernsModel(method, concernsForMethod);
                this.methodConcernsModels.Add(method, methodConcerns);
                return methodConcerns;
            }

            return this.methodConcernsModels[method];
        }

        private static void AddConcernDeclarations(Type type, List<ConcernDeclaration> concerns)
        {
            IEnumerable<ConcernsAttribute> attributes = type.GetAttributes<ConcernsAttribute>();

            IEnumerable<ConcernDeclaration> types = from attribute in attributes
                                                    from concernType in attribute.ConcernTypes
                                                    select new ConcernDeclaration(concernType, type);

            concerns.AddRange(types);
        }
    }
}