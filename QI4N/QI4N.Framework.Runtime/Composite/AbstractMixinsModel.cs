namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class AbstractMixinsModel
    {
        private readonly HashSet<Type> mixinTypes = new HashSet<Type>();

        public void AddMixinType(Type mixinType)
        {
            mixinTypes.Add(mixinType);
        }

        public MixinModel ImplementMethod(MethodInfo method)
        {
        //    throw new NotImplementedException();
            return new MixinModel();
        }

        public object[] NewMixinHolder()
        {
            return mixinTypes
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