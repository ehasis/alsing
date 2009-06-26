namespace ConsoleApplication23.Experimental
{
    using System;

    using QI4N.Framework;

    [Mixins(typeof(SayHelloMixin))]
    [Concerns(typeof(SayHelloConcern))]
    public interface SayHelloBehavior
    {
        void SayHello();
    }

    public class SayHelloMixin : SayHelloBehavior
    {
        [This]
        private SomePrivateBehavior privateMixin;

        [This]
        private HasName state;

        public void SayHello()
        {
            Console.WriteLine();
            Console.WriteLine("{0} says hello", this.state.Name);
            this.privateMixin.PrivateHello();
            Console.WriteLine();
        }
    }
}