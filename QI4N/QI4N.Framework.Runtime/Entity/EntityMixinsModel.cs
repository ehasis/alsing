namespace QI4N.Framework.Runtime
{
    public class EntityMixinsModel : AbstractMixinsModel
    {
        public EntityMixinsModel() : base(null,null)
        {
            this.mixinImplementationTypes.Add(typeof(EntityMixin));
        }
    }
}