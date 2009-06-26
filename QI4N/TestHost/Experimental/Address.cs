namespace ConsoleApplication23.Experimental
{
    using QI4N.Framework;

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

    public interface AddressValue : Address, ValueComposite, Printable
    {
    }
}