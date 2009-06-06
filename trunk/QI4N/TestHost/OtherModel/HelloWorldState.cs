namespace ConsoleApplication23.OtherModel
{
    using System;

    using QI4N.Framework;

    public interface HelloWorldState
    {
        Property<string> Name { get; }

        Property<string> Phrase { get; }
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
            return this.state.Phrase.Value + " " + this.state.Name.Value;
        }
    }

    [Mixins(typeof(HelloWorldBehaviorMixin))]
    public interface HelloWorldComposite : HelloWorldState,HelloWorldBehavior, Composite
    {
        string Say();
    }
}