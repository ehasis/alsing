namespace ConsoleApplication23
{
    using System;

    using QI4N.Framework;
    using QI4N.Framework.Bootstrap;
    using QI4N.Framework.Runtime;

    internal class Program
    {
        private static LayerAssembly CreateDomainLayer(ApplicationAssembly app)
        {
            LayerAssembly layer = app.NewLayerAssembly("DomainLayer");

            ModuleAssembly peopleModule = CreatePeopleModule(layer);

            return layer;
        }

        private static ModuleAssembly CreatePeopleModule(LayerAssembly layer)
        {
            ModuleAssembly module = layer.NewModuleAssembly("PeopleModule");

            module
                    .AddEntities()
                    .Include<CarEntity>()
                    .VisibleIn(Visibility.Layer);

            module
                    .AddServices()
                    .Include<ManufacturerRepositoryService>()
                    .VisibleIn(Visibility.Layer);

            module
                    .AddValues()
                    .WithConcern<MyGenericConcern>()
                    .Include<AccidentValue>();

            module
                    .AddTransients()
                    .Include<PersonComposite>()
                    //.WithConcern<PersonBehaviorConcern>()
                    //.WithConcern<MyGenericConcern>()
                    //.WithSideEffect<MySideEffect>()
                    .WithMixin<RandomFooMixin>();

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
            ModuleInstance peopleModule = applicationInstance.FindModule("DomainLayer", "PeopleModule");

            var factory = new TransientBuilderFactoryInstance(peopleModule);

            TransientBuilder<Person> personBuilder = factory.NewTransientBuilder<Person>();

            var personState = personBuilder.PrototypeFor<PersonState>();
            personState.FirstName.Value = "Roger";
            personState.LastName.Value = "Alsing";

            Person person = personBuilder.NewInstance();
            person.SayHi();
            Console.WriteLine(person.ToString());


            var valueFactory = new ValueBuilderFactoryInstance(peopleModule);
            var accidentBuilder = valueFactory.NewValueBuilder<Accident>();

            var accidentState = accidentBuilder.Prototype();
            accidentState.Description.Value = "hej du glade";
            accidentState.Occured.Value = new DateTime(2009, 01, 01);

            var accident = accidentBuilder.NewInstance();


            Console.ReadLine();
        }
    }
}