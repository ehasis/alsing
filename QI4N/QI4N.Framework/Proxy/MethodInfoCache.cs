namespace QI4N.Framework.Reflection
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Diagnostics;

    public static class MethodInfoCache
    {
        private static readonly IList<MethodInfo> methodLookup = new List<MethodInfo>();

        public static int AddMethod(MethodInfo methodInfo)
        {
            int methodId = methodLookup.Count;
            methodLookup.Add(methodInfo);

            return methodId;
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        public static MethodInfo GetMethod(int methodId)
        {
            return methodLookup[methodId];
        }
    }
}