namespace ConsoleApplication23
{
    using System;

    using Experimental;

    using QI4N.Framework;
    using QI4N.Framework.Bootstrap;
    using QI4N.Framework.Runtime;

    internal class Program
    {
        private static LayerAssembly CreateDomainLayer(ApplicationAssembly app)
        {
            LayerAssembly layer = app.NewLayerAssembly("DomainLayer");

            ModuleAssembly experimentalModule = CreateExperimentalModule(layer);

            return layer;
        }

        private static ModuleAssembly CreateExperimentalModule(LayerAssembly layer)
        {
            ModuleAssembly module = layer.NewModuleAssembly("ExperimentalModule");

            module
                    .AddEntities()
                    .VisibleIn(Visibility.Layer);

            module
                    .AddServices()
                    .Include<CustomerRepositoryService>()
                    .VisibleIn(Visibility.Layer);

            module
                    .AddValues()
                    .Include<AddressValue>();

            module
                    .AddTransients()
                    .Include<CustomerTransient>()
                    .WithConcern<GenericTracingConcern>()
                    //  .WithConcern<SayHelloConcern>()
                    .WithSideEffect<SayHelloSideEffect>();

            return module;
        }

        private static void Main()
        {
            var f = new ApplicationAssemblyFactory();

            ApplicationAssembly app = f.NewApplicationAssembly();

            LayerAssembly domainLayer = CreateDomainLayer(app);

            ApplicationModel applicationModel = ApplicationModel.NewModel(app);

            ApplicationInstance applicationInstance = applicationModel.NewInstance();

            Run(applicationInstance);
        }

        private static void Run(ApplicationInstance applicationInstance)
        {
            ModuleInstance peopleModule = applicationInstance.FindModule("DomainLayer", "ExperimentalModule");

            var factory = new TransientBuilderFactoryInstance(peopleModule);
            var addressFactory = new ValueBuilderFactoryInstance(peopleModule);

            TransientBuilder<Customer> customerBuilder = factory.NewTransientBuilder<Customer>();
            ValueBuilder<Address> addressBuilder = addressFactory.NewValueBuilder<Address>();

            Address protoAddress = addressBuilder.Prototype();
            protoAddress.City = "Foo City";
            protoAddress.StreetName = "Acme road 123";
            protoAddress.ZipCode = "888-555";

            var protoCustomer = customerBuilder.PrototypeFor<Customer>();
            protoCustomer.Name = "Acme Inc";
            protoCustomer.Email = "Roger.Alsing@Precio.se";
            protoCustomer.Address = addressBuilder.NewInstance();

            Customer customer = customerBuilder.NewInstance();

            customer.Print();

            customer.SayHello();

            //customer.Address.City = "abc"; //should throw, immutable object

            Address otherAddress = addressBuilder.NewInstance();

            bool areEqual = customer.Address.Equals(otherAddress);

            if (areEqual)
            {
                Console.WriteLine("customer.Address and otherAddress are equal");
            }

            //customer.SayHelloTo(null); //should throw, name is not optional

            customer.SayHelloTo("Roger");

            Console.ReadLine();
        }
    }
}