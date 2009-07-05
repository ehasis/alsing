namespace ConsoleApplication23.Experimental
{
    using System;

    using QI4N.Framework;

    public class SayHelloConcern : ConcernOf<SayHelloBehavior>, SayHelloBehavior
    {
        public void SayHello()
        {
            Console.WriteLine("This is the SayHello Concern speaking");
            this.next.SayHello();
        }

        public void SayHelloTo(string name)
        {
            this.next.SayHelloTo(name);
        }
    }
}