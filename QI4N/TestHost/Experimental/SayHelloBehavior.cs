namespace ConsoleApplication23.Experimental
{
    using System;

    using QI4N.Framework;

    [Mixins(typeof(SayHelloBehaviorMixin))]
    [Concerns(typeof(SayHelloConcern))]
    public interface SayHelloBehavior
    {
        void SayHello();
    }

    public class SayHelloBehaviorMixin : SayHelloBehavior
    {
        [This]
        private SomePrivateBehavior privateMixin;

        [This]
        private HasName state;

        public void SayHello()
        {
            Console.WriteLine("{0} says hello", this.state.Name);
            this.privateMixin.PrivateHello();
        }
    }
}