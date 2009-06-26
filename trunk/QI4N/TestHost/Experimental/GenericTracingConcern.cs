namespace ConsoleApplication23.Experimental
{
    using System;
    using System.Reflection;

    using QI4N.Framework;

    public class GenericTracingConcern : GenericConcern
    {
        public override object Invoke(object proxy, MethodInfo method, object[] args)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Entering method {0}", method.Name);
            Console.ForegroundColor = color;

            object res = this.next.Invoke(proxy, method, args);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (method.ReturnType != typeof(void))
                Console.WriteLine("Exiting method {0} with result '{1}'", method.Name, res);
            else
                Console.WriteLine("Exiting method {0}", method.Name);
            Console.ForegroundColor = color;

            return res;
        }
    }
}