namespace ConsoleApplication23.Experimental
{
    using System;
    using System.Reflection;

    using QI4N.Framework;

    public class GenericTracingConcern : GenericConcern
    {
        public override object Invoke(object proxy, MethodInfo method, object[] args)
        {
            Console.WriteLine("   Entering method {0}", method.Name);
            object res = this.next.Invoke(proxy, method, args);

            if (method.ReturnType != typeof(void))
                Console.WriteLine("   Exiting method {0} with result '{1}'", method.Name, res);
            else
                Console.WriteLine("   Exiting method {0}", method.Name);
            return res;
        }
    }
}