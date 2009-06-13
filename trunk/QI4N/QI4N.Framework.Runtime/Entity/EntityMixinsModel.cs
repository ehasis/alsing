namespace QI4N.Framework.Runtime
{
    public class EntityMixinsModel : MixinsModel
    {
        public EntityMixinsModel()
        {
            this.mixinImplementationTypes.Add(typeof(EntityMixin));
        }
    }
}