namespace ConsoleApplication23
{
    using System;

    using QI4N.Framework;

    internal class Program
    {
        private static void Main()
        {
            CompositeBuilderFactory factory = new DefaultCompositeBuilderFactory();
            var carBuilder = factory.NewCompositeBuilder<Car>();
            var manufacturerBuilder = factory.NewCompositeBuilder<Manufacturer>();
            var modelBuilder = new DefaultObjectBuilder<Model>();
            var accidentBuilder = factory.NewCompositeBuilder<Accident>();

            var protoManufacturer = manufacturerBuilder.StateOfComposite();
            protoManufacturer.Country.Value = "Sweden";
            protoManufacturer.Name.Value = "Volvo";
            protoManufacturer.CarsProduced.Value = 1234;

            var manufacturer = manufacturerBuilder.NewInstance();

            Model model = modelBuilder.NewInstance();
            model.Set("Amazon");

            var protoCar = carBuilder.StateOfComposite();            
            protoCar.Model.Set("Amazon");            

            // Associations are not available to plain composites
            // protoCar.Manufacturer.Set(manufacturer); 

            var car = carBuilder.NewInstance();

            Console.WriteLine(car.Model.Get());

            var protoAccident = accidentBuilder.StateOfComposite();
            protoAccident.Description.Value = "Roger fell off the chair";
            protoAccident.Occured.Value = new DateTime(2009, 06, 01);
            protoAccident.Repaired.Value = new DateTime(2010, 01, 01);

            var accident = accidentBuilder.NewInstance();

            car.Accidents.Add(accident);

            Console.WriteLine(manufacturer.CarsProduced.Get());
            foreach(var a in car.Accidents)
            {
                Console.WriteLine(a.Description.Get());
                Console.WriteLine(a.GetType().Name);
            }

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