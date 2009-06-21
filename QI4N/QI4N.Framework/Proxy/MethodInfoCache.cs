namespace QI4N.Framework.Reflection
{
    using System.Collections.Generic;
    using System.Reflection;

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

#if !DEBUG
        [DebuggerStepThrough]
        [DebuggerHidden]
#endif

        public static MethodInfo GetMethod(int methodId)
        {
            return methodLookup[methodId];
        }
    }
}