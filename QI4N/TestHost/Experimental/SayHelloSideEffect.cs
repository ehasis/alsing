namespace ConsoleApplication23.Experimental
{
    using System;

    using QI4N.Framework;

    public class SayHelloSideEffect : SideEffectOf<SayHelloBehavior>, SayHelloBehavior
    {
        public void SayHello()
        {
            Console.WriteLine("This is the SayHello SideEffect");
            this.result.SayHello();
        }

        public void SayHelloTo(string name)
        {
            this.result.SayHelloTo(name);
        }
    }
}