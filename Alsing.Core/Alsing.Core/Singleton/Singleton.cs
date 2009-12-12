using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alsing
{
    public static class Singleton<T> where T : new()
    {
        public static T Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        private static class Nested
        {
            public static readonly T instance = new T();
        }
    }
}
