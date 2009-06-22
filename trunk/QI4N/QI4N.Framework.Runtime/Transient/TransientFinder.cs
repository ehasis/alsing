namespace QI4N.Framework.Runtime
{
    public sealed class TransientFinder : TypeFinder<TransientModel>
    {
        protected override TransientModel FindModel(ModuleModel model, Visibility visibility)
        {
            return model.Transients.GetCompositeModelFor(this.MixinType, visibility);
        }
    }
}