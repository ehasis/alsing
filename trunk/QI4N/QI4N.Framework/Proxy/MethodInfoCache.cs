namespace QI4N.Framework.Reflection
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Diagnostics;

    public static class MethodInfoCache
    {
        private static readonly IList<MethodInfo> methodLookup = new List<MethodInfo>();

        private static readonly object syncRoot = new object();

        public static int AddMethod(MethodInfo methodInfo)
        {
            lock (syncRoot)
            {
                int methodId = methodLookup.Count;
                methodLookup.Add(methodInfo);

                return methodId;
            }
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static MethodInfo GetMethod(int methodId)
        {
            return methodLookup[methodId];
        }
    }
}