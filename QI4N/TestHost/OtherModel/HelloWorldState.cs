namespace ConsoleApplication23.OtherModel
{
    using System;

    using QI4N.Framework;

    [Mixins(typeof(HelloWorldStateMixin))]
    public interface HelloWorldState
    {
        Property<string> Name { get; }

        Property<string> Phrase { get; }
    }

    public class HelloWorldStateMixin : HelloWorldState
    {
        [State]
        private Property<string> name;

        [State]
        private Property<string> phrase;

        public Property<string> Name
        {
            get
            {
                return name;
            }
        }

        public Property<string> Phrase
        {
            get
            {
                return phrase;
            }
        }
    }


    public class HelloWorldMixin : HelloWorldComposite
    {
        [This]
        private HelloWorldState state;

        public String Say()
        {
            return this.state.Phrase.Value + " " + this.state.Name.Value;
        }
    }

    [Mixins(typeof(HelloWorldMixin))]
    public interface HelloWorldComposite : Composite
    {
        string Say();
    }
}