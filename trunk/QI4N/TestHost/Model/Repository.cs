namespace ConsoleApplication23
{
    using System.Linq;

    using QI4N.Framework;

    public interface ManufacturerRepository
    {
        Manufacturer FindByIdentity(string identity);

        Manufacturer FindByName(string name);
    }

    [Mixins(typeof(ManufacturerRepositoryMixin))]
    public interface ManufacturerRepositoryService : ManufacturerRepository, ServiceComposite
    {
    }

    public class ManufacturerRepositoryMixin : ManufacturerRepository
    {
        [Structure]
        private UnitOfWorkFactory uowf;

        public Manufacturer FindByIdentity(string identity)
        {
            UnitOfWork uow = uowf.CurrentUnitOfWork;
            return uow.Find<Manufacturer>(identity);            
        }

        public Manufacturer FindByName(string name)
        {
            UnitOfWork uow = uowf.CurrentUnitOfWork;

            var result = from m in uow.NewQuery<Manufacturer>()
                         where m.Name.Value == name
                         select m;

            return result.FirstOrDefault();
        }
    }
}