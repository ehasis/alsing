namespace CompositeDiagrammer
{
    using System;
    using System.Reflection;

    using QI4N.Framework;

    public class GenericStateTrackingConcern : GenericConcern
    {
        public override object Invoke(object proxy, MethodInfo method, object[] args)
        {
            object res = next.Invoke(proxy, method, args);
            Console.WriteLine("{0} - {1}",method.Name,args);

            return res;
        }
    }
}