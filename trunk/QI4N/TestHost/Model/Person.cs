namespace ConsoleApplication23
{
    using System;
    using System.Reflection;

    using QI4N.Framework;

    [Concerns(typeof(PersonBehaviorConcern), typeof(MyGenericConcern))]
    [SideEffects(typeof(MySideEffect))]
    [Mixins(typeof(PersonBehaviorMixin))]
    public interface PersonComposite : Person, TransientComposite
    {
    }

  //  [SideEffects(typeof(MySideEffect))]
    public interface PersonBehavior
    {
        void SayHi();
    }

    [Mixins(typeof(OinkOinkMixin))]
    public interface OinkOink
    {
        void Oink();
    }

    public class OinkOinkMixin : OinkOink
    {
        [This]
        private PersonState state;

        public void Oink()
        {
            Console.WriteLine("OinkOink {0}", this.state.FirstName);
        }
    }

    public class PersonBehaviorMixin : PersonBehavior
    {
        [Uses]
        private string email;

        [This]
        private OinkOink oink;

        [This]
        private PersonState self;

        public void SayHi()
        {
            Console.WriteLine("{0} {1} Says hello from QI4N - email {2}", this.self.FirstName, this.self.LastName, this.email);

            this.oink.Oink();
        }
    }

    public interface Person : PersonBehavior
    {
    }

    public interface HasAddress
    {
        string Street { get; set; }

        string City { get; set; }

        int ZipCode { get; set; }
    }

    public interface HasName
    {
        string FirstName { get; set; }

        string LastName { get; set; }
    }


    public interface PersonState : HasName, HasAddress
    {
        DateTime BirthDate { get; set; }

        double Weight { get; set; }
    }


    public class MyGenericConcern : GenericConcern
    {
        public override object Invoke(object proxy, MethodInfo method, object[] args)
        {
            Console.WriteLine("Before {0}", method.Name);
            object res = this.next.Invoke(proxy, method, args);
            Console.WriteLine("After {0}", method.Name);
            return res;
        }
    }

    public class PersonBehaviorConcern : ConcernOf<PersonBehavior>, PersonBehavior
    {
        public void SayHi()
        {
            Console.WriteLine("Before say hi-----");
            this.next.SayHi();
            Console.WriteLine("After say hi-----");
        }
    }

    public interface RandomFoo
    {
        void Foo();
    }

    public class RandomFooMixin : RandomFoo
    {
        public void Foo()
        {
            Console.WriteLine("FOO FOO");
        }
    }

    public class MySideEffect : SideEffectOf<PersonBehavior>, PersonBehavior
    {
        public void SayHi()
        {
            Console.WriteLine("--SIDEEFFECT OF SaY HI---");
        }
    }
}