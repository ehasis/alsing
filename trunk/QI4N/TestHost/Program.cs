namespace ConsoleApplication23
{
    using System;

    using QI4N.Framework;

    internal class Program
    {
        private static void Main()
        {
            CompositeBuilderFactory factory = new DefaultCompositeBuilderFactory();
            
            var modelBuilder = new DefaultObjectBuilder<Model>();

            var manufacturer = factory.NewComposite<Manufacturer>();
            manufacturer.Country.Set("Sweden");
            manufacturer.CarsProduced.Set(77);
            manufacturer.Name.Set("VOLVO");

            Model model = modelBuilder.NewInstance();
            model.Set("Amazon");

            var carBuilder = factory.NewCompositeBuilder<Car>();

            var protoCar = carBuilder.StateOfComposite();
            protoCar.Model.Set("Amazon");            
        //    protoCar.Manufacturer.Set(manufacturer);

            var car = carBuilder.NewInstance();

            var accident = factory.NewComposite<Accident>();
            accident.Description.Set("Roger fell off the chair");
            accident.Occured.Set(new DateTime(2009, 06, 01));
            accident.Repaired.Set(new DateTime(2010, 01, 01));
            
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