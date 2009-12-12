using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alsing.Serialization.Tests.Classes
{
    public class TestClass
    {
        public IDictionary<string,int> DictionaryProperty { get; set; }
        public IList<string> ListProperty { get; set; }
        public int[] ArrayProperty { get; set; }
        public int PrimitiveProperty { get; set; }
        public DateTime? NullableProperty { get; set; }
        public ClassA ReferenceProperty { get; set; }
    }

    public class ClassA
    {
        public ClassB B { get; set; }
    }

    public class ClassB
    {
        public ClassA A { get; set; }
    }
}
