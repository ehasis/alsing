namespace QI4N.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class CompositeTypeCache
    {
        internal static readonly Type[] Composites;

        static CompositeTypeCache()
        {
            IEnumerable<Type> types = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                      from type in assembly.GetTypes()
                                      where typeof(Composite).IsAssignableFrom(type)
                                      select type;

            Composites = types.ToArray();
        }
    }
}