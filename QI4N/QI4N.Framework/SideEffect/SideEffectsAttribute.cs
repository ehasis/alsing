namespace QI4N.Framework
{
    using System;

    public class SideEffectsAttribute : Attribute
    {
        public SideEffectsAttribute(params Type[] sideEffectTypes)
        {
            this.SideEffectTypes = sideEffectTypes;
        }

        public Type[] SideEffectTypes { get; set; }
    }
}