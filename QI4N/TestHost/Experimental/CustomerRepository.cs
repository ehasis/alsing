namespace ConsoleApplication23.Experimental
{
    using System.Collections.Generic;
    using System.Linq;

    using QI4N.Framework;

    public interface CustomerRepository
    {
        Customer FindByIdentity(string identity);

        Customer FindByName(string name);

        Customer NewCustomer(string name);
    }

    [Mixins(typeof(CustomerRepositoryMixin))]
    public interface CustomerRepositoryService : CustomerRepository, ServiceComposite
    {
    }

    public class CustomerRepositoryMixin : CustomerRepository
    {
        [Structure]
        private TransientBuilderFactory tbf;

        [Structure]
        private UnitOfWorkFactory uowf;

        public Customer FindByIdentity(string identity)
        {
            UnitOfWork uow = this.uowf.CurrentUnitOfWork;
            return uow.Find<Customer>(identity);
        }

        public Customer FindByName(string name)
        {
            UnitOfWork uow = this.uowf.CurrentUnitOfWork;

            IEnumerable<Customer> result = from customer in uow.NewQuery<Customer>()
                                           where customer.Name == name
                                           select customer;

            return result.FirstOrDefault();
        }

        public Customer NewCustomer(string name)
        {
            var customer = tbf.NewTransient<Customer>();
            customer.Name = name;
            return customer;
        }
    }
}