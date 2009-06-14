namespace QI4N.Framework.Runtime
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    public class MixinModel
    {
        public MixinsModel MixinsModel { get; set; }

        public Type MixinType { get; set; }

        public object NewInstance(CompositeInstance compositeInstance, StateHolder stateHolder, UsesInstance uses)
        {
            throw new NotImplementedException();
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public FragmentInvocationHandler NewInvocationHandler(MethodInfo method)
        {
            if (typeof(InvocationHandler).IsAssignableFrom(this.MixinType))
            {
                return new GenericFragmentInvocationHandler();
            }

            return new TypedFragmentInvocationHandler();
        }
    }
}