namespace ConsoleApplication23.Experimental
{
    using System;

    using QI4N.Framework;

    public interface CustomerTransient : Customer, TransientComposite
    {
    }

    public interface Customer : HasName, HasAddress, HasEmail, Printable
    {
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
            foreach(Property p in state.GetProperties())
            {
                var value = p.Value;
                if (value is Printable)
                {
                    ((Printable)value).Print();
                }
                else
                {
                    Console.WriteLine(p.Value);
                }                
            }
        }
    }
}