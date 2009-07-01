namespace ConsoleApplication23.Experimental
{
    using System;

    using QI4N.Framework;

    public class SayHelloSideEffect : SideEffectOf<SayHelloBehavior>, SayHelloBehavior
    {
        public void SayHello()
        {
            Console.WriteLine("This is the SayHello SideEffect");
            this.next.SayHello();
        }

        #region SayHelloBehavior Members


        public void SayHelloTo(string name)
        {
            this.next.SayHelloTo(name);
        }

        #endregion
    }
}