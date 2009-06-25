namespace ConsoleApplication23.OtherModel
{
    using System;

    using QI4N.Framework;

    public interface HelloWorldState
    {
        string Name { get; }

        string Phrase { get; }
    }

    public interface HelloWorldBehavior
    {
        string Say();
    }


    public class HelloWorldBehaviorMixin : HelloWorldBehavior
    {
        [This]
        private HelloWorldState state;

        public String Say()
        {
            return this.state.Phrase + " " + this.state.Name;
        }
    }

    [Mixins(typeof(HelloWorldBehaviorMixin))]
    public interface HelloWorldComposite : HelloWorldState, HelloWorldBehavior, Composite
    {
    }
}