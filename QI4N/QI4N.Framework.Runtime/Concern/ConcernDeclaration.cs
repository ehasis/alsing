namespace QI4N.Framework.Runtime
{
    using System;

    public class ConcernDeclaration : AbstractModifierDeclaration
    {
        public ConcernDeclaration(Type modifierClass, Type declaredIn)
                : base(modifierClass, declaredIn)
        {
        }

        public override string ToString()
        {
            return "Concern " + base.ToString();
        }
    }
}