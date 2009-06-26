namespace ConsoleApplication23.Experimental
{
    using System;

    using QI4N.Framework;

    [Mixins(typeof(SomePrivateBehaviorMixin))]    
    public interface SomePrivateBehavior
    {
        void PrivateHello();
    }

    public class SomePrivateBehaviorMixin : SomePrivateBehavior
    {
        public void PrivateHello()
        {
            Console.WriteLine("This method cannot be activated from the proxy");
        }
    }
}