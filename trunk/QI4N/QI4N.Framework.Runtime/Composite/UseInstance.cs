namespace QI4N.Framework
{
    using System.Collections.Generic;

    public class UsesInstance
    {
        public static readonly UsesInstance NO_USES = new UsesInstance();

        protected List<object> usedObjects = new List<object>();

        public void Use(object[] usedObjects)
        {
            this.usedObjects.AddRange(usedObjects);
        }
    }
}