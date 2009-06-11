namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class MixinModel
    {
        public Type MixinType { get; set; }

        public MixinsModel MixinsModel { get; set; }

        public object NewInstance(CompositeInstance compositeInstance, StateHolder stateHolder, UsesInstance uses)
        {
            throw new NotImplementedException();
        }

        public FragmentInvocationHandler NewInvocationHandler(MethodInfo method)
        {
            return new DefaultFragmentInvocationHandler();
        }
    }
}