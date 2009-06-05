namespace ConsoleApplication23
{
    using System;

    using OtherModel;

    using QI4N.Framework;

    internal class Program
    {
        private static void Main()
        {
            //var factory = new DefaultCompositeBuilderFactory();
            //var carFactory = factory.NewComposite<CarEntityFactory>();
            //var car = carFactory.Create(null, "");

            var factory = new DefaultCompositeBuilderFactory();
            var helloWorld = factory.NewComposite<HelloWorldBehaviour>();
            Console.WriteLine(helloWorld.Say());

            //var modelBuilder = new DefaultObjectBuilder<Model>();
            //Model model = modelBuilder.NewInstance();

            //model.Value = "hej";
            //Console.WriteLine(model.Value);
            //Console.WriteLine(model.IsComputed);
            //Console.WriteLine(model.IsMutable);

            //var manuBuilder = new DefaultCompositeBuilder<Manufacturer>();
            //Manufacturer manufacturer = manuBuilder.NewInstance();
            //manufacturer.Country.Value = "swe";

            //CompositeBuilderFactory factory = new DefaultCompositeBuilderFactory();
            //var car = factory.NewComposite<Car>();

            ////       car.Model.Value = model.Value;

            //var icar = car as Identity;
            //icar.Identity.Value = "tjorven";

            //Console.WriteLine(icar.Identity.Value);


            //ObjectBuilderFactory factory = new DefaultObjectBuilderFactory();

            //var manufacturerBuilder = factory.NewObjectBuilder<Manufacturer>();
            //var carBuilder = factory.NewObjectBuilder<CarEntity>();

            //Manufacturer manufacturer = manufacturerBuilder.NewInstance();
            //Car car = carBuilder.NewInstance();   //carEntityFactory.Create(manufacturer,"v70");

            //manufacturer.Country.Value = "hej";  
            //car.Manufacturer.Value = manufacturer;
        }
    }
}