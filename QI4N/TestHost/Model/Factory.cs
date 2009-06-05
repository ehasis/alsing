using QI4N.Framework;
namespace ConsoleApplication23
{
    public interface CarEntityFactory
    {
        Car Create(Manufacturer manufacturer, string model);
    }

    [Mixins(typeof(CarEntityFactoryMixin))]
    public interface CarEntityFactoryService : CarEntityFactory, ServiceComposite
    {
    }

    public class CarEntityFactoryMixin : CarEntityFactory
    {
        [Structure]
        private UnitOfWorkFactory uowf;

        public Car Create(Manufacturer manufacturer, string model)
        {
            UnitOfWork uow = this.uowf.CurrentUnitOfWork;

            EntityBuilder<Car> builder = uow.NewEntityBuilder<Car>();

            Car prototype = builder.StateFor();
            prototype.Manufacturer.Value = manufacturer;
            prototype.Model.Value = model;

            return builder.NewInstance();
        }
    }
}