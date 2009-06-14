namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class UsesInstance
    {
        public static readonly UsesInstance NO_USES = new UsesInstance();

        protected List<object> usedObjects = new List<object>();

        public void Use(object[] usedObjects)
        {
            this.usedObjects.AddRange(usedObjects);
        }

        public object UseForType( Type type )
        {
            foreach(var obj in usedObjects)
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