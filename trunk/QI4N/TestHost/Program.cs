namespace ConsoleApplication23
{
    using System;

    using QI4N.Framework;
    using QI4N.Framework.Bootstrap;
    using QI4N.Framework.Runtime;

    internal class Program
    {
        private static void Main()
        {
            Qi4nRuntime qi4j;

            ApplicationAssembly app = qi4j.NewApplicationAssembly();

            LayerAssembly runtimeLayer = CreateRuntimeLayer(app);
            LayerAssembly designerLayer = CreateDesignerLayer(app);
            LayerAssembly domainLayer = CreateDomainLayer(app);
            LayerAssembly messagingLayer = CreateMessagingLayer(app);
            LayerAssembly persistenceLayer = CreatePersistenceLayer(app);

            // declare structure between layers
            domainLayer.Uses(messagingLayer);
            domainLayer.Uses(persistenceLayer);
            designerLayer.Uses(persistenceLayer);
            designerLayer.Uses(domainLayer);
            runtimeLayer.Uses(domainLayer);

            // Instantiate the Application Model.
            application = qi4j.NewApplication(app);




     //       var moduleInstance = new ModuleInstance();
     //       var factory = new TransientBuilderFactoryInstance(moduleInstance);
     //       TransientBuilder<Person> personFactory = factory.NewTransientBuilder<Person>();

     //       personFactory.Use("Roger@Alsing.com");
     //       var protoPerson = personFactory.PrototypeFor<PersonState>();
     //       protoPerson.FirstName.Value = "Roger";
     //       protoPerson.LastName.Value = "Alsing";
     //       protoPerson.Weight.Value = 85;

     //       Person person = personFactory.NewInstance();

     // //      Console.WriteLine(person.Weight.Value);

     //       Person otherPerson = personFactory.NewInstance();

     ////       otherPerson.Weight.Value = 99;

     ////       Console.WriteLine(person.Weight.Value);
     ////       Console.WriteLine(otherPerson.Weight.Value);
            
     //       person.SayHi();

     //       Console.ReadLine();

            //// Lacking support for QI4J structural definitions
            //// just kickstart my default impl
            //var moduleInstance = new ModuleInstance();
            //var factory = new CompositeBuilderFactoryInstance(moduleInstance);

            //// Create composite builder
            //var carBuilder = factory.NewCompositeBuilder<Car>();
            //var manufacturerBuilder = factory.NewCompositeBuilder<Manufacturer>();
            //var accidentBuilder = factory.NewCompositeBuilder<Accident>();

            //// prototype support is in place and works
            //var protoManufacturer = manufacturerBuilder.Prototype();

            //// Properties support .Value and Get/Set
            //// Set the properties of the prototype
            //protoManufacturer.Country.Value = "Sweden";
            //protoManufacturer.Name.Value = "Volvo";
            //protoManufacturer.CarsProduced.Value = 1234;    

            //// Create an instance based on the prototype
            //var manufacturer = manufacturerBuilder.NewInstance();

            //// Create a prototype for a car composite
            //var protoCar = carBuilder.Prototype();
            //protoCar.Model.Value = "Amazon";

            //// create a car composite based on the prototype
            //var car = carBuilder.NewInstance();
            //car.Manufacturer.Set(manufacturer);

            //var idCar = (Identity)car;
            //Console.WriteLine(idCar.Identity.Value);

            //// create a prototype value object
            //var protoAccident = accidentBuilder.Prototype();
            //protoAccident.Description.Value = "Wheel fell off";
            //protoAccident.Occured.Value = new DateTime(2009, 06, 01);
            //protoAccident.Repaired.Value = new DateTime(2010, 01, 01);           
            //// instance the value object
            //var accident = accidentBuilder.NewInstance();

            //// Add a value to an "ManyAssociation"
            //car.Accidents.Add(accident);

            //Console.WriteLine(manufacturer.CarsProduced.Value);
            //foreach (var a in car.Accidents)
            //{
            //    Console.WriteLine(a.Description.Value);
            //}

            //////       car.Model.Value = model.Value;

            ////var icar = car as Identity;
            ////icar.Identity.Value = "tjorven";

            ////Console.WriteLine(icar.Identity.Value);

            ////ObjectBuilderFactory factory = new DefaultObjectBuilderFactory();

            ////var manufacturerBuilder = factory.NewObjectBuilder<Manufacturer>();
            ////var carBuilder = factory.NewObjectBuilder<CarEntity>();

            ////Manufacturer manufacturer = manufacturerBuilder.NewInstance();
            ////Car car = carBuilder.NewInstance();   //carEntityFactory.Create(manufacturer,"v70");

            ////manufacturer.Country.Value = "hej";  
            ////car.Manufacturer.Value = manufacturer;
        }

        private static LayerAssembly CreatePersistenceLayer(ApplicationAssembly app)
        {
            throw new NotImplementedException();
        }

        private static LayerAssembly CreateMessagingLayer(ApplicationAssembly app)
        {
            throw new NotImplementedException();
        }

        private static LayerAssembly CreateDomainLayer(ApplicationAssembly app)
        {
            LayerAssembly layer = app.NewLayerAssembly();

            ModuleAssembly peopleModule = CreatePeopleModule(layer);
            return layer;


        }

        private static ModuleAssembly CreatePeopleModule(LayerAssembly layer)
        {
            ModuleAssembly module = layer.NewModuleAssembly();
    
            module.AddEntity<CarEntity>();
            module.AddService<ManufacturerRepositoryService>().VisibleIn( Visibility.Layer );
            module.AddValue<AccidentValue>();
            module.AddTransient<PersonComposite>();

            return module;


        }

        private static LayerAssembly CreateDesignerLayer(ApplicationAssembly app)
        {
            throw new NotImplementedException();
        }

        private static LayerAssembly CreateRuntimeLayer(ApplicationAssembly app)
        {
            throw new NotImplementedException();
        }
    }
}