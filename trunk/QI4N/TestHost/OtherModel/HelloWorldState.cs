using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication23.OtherModel
{
    using QI4N.Framework;

    public interface HelloWorldState
    {
        Property<string> Name { get; }
        Property<string> Phrase { get; }
    }

    public interface HelloWorldBehaviour
    {
        string Say();
    }

    public class HelloWorldBehaviourMixin : HelloWorldBehaviour
    {
        [This]
        private HelloWorldState state;

        public String Say()
        {
            return state.Phrase.Value + " " + state.Name.Value;
        }
    }

    [Mixins(typeof(HelloWorldBehaviourMixin))]
    public interface HelloWorldComposite : HelloWorldBehaviour, HelloWorldState, Composite
    {

    }
}
