namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;

    public class UnitOfWorkInstance
    {
        [ThreadStatic]
        private static Stack<UnitOfWorkInstance> current;

        public static Stack<UnitOfWorkInstance> Current
        {
            get
            {
                if (current == null)
                {
                    current = new Stack<UnitOfWorkInstance>();
                }

                return current;
            }
        }
    }
}