namespace ConsoleApplication23.Experimental
{
    using System;
    using System.Linq;

    using QI4N.Framework;

    public interface CustomerTransient : Customer, TransientComposite
    {
    }

    public interface Customer : HasName, HasAddress, HasEmail, Printable , CustomerBehavior
    {
    }

    [Mixins(typeof(CustomerBehaviorMixin))]
    public interface CustomerBehavior
    {
        void SayHello();
    }

    public class CustomerBehaviorMixin : CustomerBehavior
    {
        [This]
        private Customer state;

        [This]
        private SomePrivateBehavior privateMixin;

        public void SayHello()
        {
            Console.WriteLine();
            Console.WriteLine("{0} says hello",state.Name);
            privateMixin.PrivateHello();
            Console.WriteLine();
        }
    }

    [Mixins(typeof(SomePrivateBehaviorMixin))]
    public interface SomePrivateBehavior
    {
        void PrivateHello();
    }

    public class SomePrivateBehaviorMixin : SomePrivateBehavior
    {

        #region SomePrivateBehavior Members

        public void PrivateHello()
        {
            Console.WriteLine("This method cannot be activated from the proxy");
        }

        #endregion
    }

    public interface HasName
    {
        string Name { get; set; }
    }

    public interface HasEmail
    {
        string Email { get; set; }
    }

    public interface HasAddress
    {
        Address Address { get; set; }
    }

    public interface Address
    {
        string StreetName { get; set; }

        string City { get; set; }

        string ZipCode { get; set; }
    }

    public interface AddressValue : Address, ValueComposite , Printable
    {
    }

    [Mixins(typeof(PrintableMixin))]
    public interface Printable
    {
        void Print();
    }

    public class PrintableMixin : Printable
    {
        [State]
        private StateHolder state;

        public void Print()
        {
            foreach(Property property in state.GetProperties().OrderBy(p => p.QualifiedName))
            {
                var value = property.Value;
                if (value is Printable)
                {
                    Console.WriteLine();
                    Console.WriteLine("Sub Property {0}:",property.QualifiedName);
                    ((Printable)value).Print();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("{0} = {1}",property.QualifiedName,property.Value);
                }                
            }
        }
    }
}