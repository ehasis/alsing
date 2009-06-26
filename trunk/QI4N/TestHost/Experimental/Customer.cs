namespace ConsoleApplication23.Experimental
{
    using QI4N.Framework;

    //[Concerns(typeof(GenericTracingConcern))]
    public interface CustomerTransient : Customer, TransientComposite
    {
    }

    public interface Customer : HasName, HasAddress, HasEmail, Printable, SayHelloBehavior
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
}