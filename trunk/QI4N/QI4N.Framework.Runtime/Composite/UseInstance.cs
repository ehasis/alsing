namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class UsesInstance
    {
        public static readonly UsesInstance NoUses = new UsesInstance();

        protected List<object> usedObjects = new List<object>();

        //[DebuggerStepThrough]
        ////[DebuggerHidden]
        public void Use(params object[] usedObjects)
        {
            this.usedObjects.AddRange(usedObjects);
        }

        public object UseForType(Type type)
        {
            foreach (object obj in this.usedObjects)
            {
                if (type.IsAssignableFrom(obj.GetType()))
                {
                    return obj;
                }
            }

            return null;
        }
    }
}