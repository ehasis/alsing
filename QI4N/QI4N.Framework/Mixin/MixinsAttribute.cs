namespace QI4N.Framework
{
    using System;

    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = true)]
    public sealed class MixinsAttribute : Attribute
    {
        public MixinsAttribute(params Type[] mixinTypes)
        {
            this.MixinTypes = mixinTypes;
        }

        public Type[] MixinTypes { get; private set; }
    }
}