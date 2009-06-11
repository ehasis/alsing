namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Reflection;

    public class AbstractMixinsModel
    {
        private readonly HashSet<Type> mixinTypes = new HashSet<Type>();

        private readonly HashSet<Type> mixinImplementationTypes = new HashSet<Type>();

        public void AddMixinType(Type mixinType)
        {
            mixinTypes.Add(mixinType);

            foreach(Type mixinImplementationType in mixinType.GetMixinTypes())
            {
                mixinImplementationTypes.Add(mixinImplementationType);
            }
        }

        public MixinModel ImplementMethod(MethodInfo method)
        {
            return new MixinModel();
        }

        public object[] NewMixinHolder()
        {
            // TODO: linqify
            return mixinImplementationTypes
                    .Select(type => Activator.CreateInstance(type, null))
                    .ToArray();
        }
    }

    public class MixinModel
    {
        public FragmentInvocationHandler NewInvocationHandler(MethodInfo method)
        {
            return new DefaultFragmentInvocationHandler();
        }
    }
}