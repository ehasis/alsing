namespace QI4N.Framework.Runtime
{
    public class EntityMixin : Entity
    {
        [This]
        private EntityComposite meAsEntity;

        public UnitOfWork UnitOfWork
        {
            get
            {
                EntityInstance instance = EntityInstance.GetEntityInstance(this.meAsEntity);
                return instance.UnitOfWork();
            }
        }
    }
}