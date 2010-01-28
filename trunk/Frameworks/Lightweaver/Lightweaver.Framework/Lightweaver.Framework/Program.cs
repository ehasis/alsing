using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace Lightweaver.Framework
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            var weaver = new Weaver();

            var person = weaver.CreateProxy( x => new Person("Roger"));
            person.Name = "Roger";           
        }

        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }
    }

    public class Person
    {
        public Person(string name)
        {
            Name = name;
        }

        public virtual string Name { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual void SayHello(string foo)
        {
            Console.WriteLine("hello {0}", foo);
        }
    }

}
