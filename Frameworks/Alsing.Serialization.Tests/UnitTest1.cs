using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Alsing.Serialization.Tests.Classes;

namespace Alsing.Serialization.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            SerializerEngine engine = new SerializerEngine();
            MemoryStream stream = new MemoryStream();

            TestClass root = new TestClass();
            root.ListProperty = new List<string>();
            root.DictionaryProperty = new Dictionary<string, int>();
            root.ArrayProperty = new int[10];
            for (int i = 0; i < 10; i++)
                root.ArrayProperty[i] = 1000 + i;

            root.NullableProperty = DateTime.Now;
            root.PrimitiveProperty = 123;
            root.ReferenceProperty = new ClassA();
            root.ReferenceProperty.B = new ClassB();
            root.ReferenceProperty.B.A = root.ReferenceProperty;

            root.DictionaryProperty.Add("a", 1);
            root.DictionaryProperty.Add("b", 2);
            root.DictionaryProperty.Add("c", 3);

            engine.Serialize(stream, root);

            stream.Position = 0;
            StreamReader sr = new StreamReader(stream);
            string result = sr.ReadToEnd();


        }
    }
}
