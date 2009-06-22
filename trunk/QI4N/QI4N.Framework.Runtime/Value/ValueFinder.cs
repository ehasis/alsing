namespace QI4N.Framework.Runtime
{
    public sealed class ValueFinder : TypeFinder<ValueModel>
    {
        protected override ValueModel FindModel(ModuleModel model, Visibility visibility)
        {
            return model.Values.GetValueModelFor(this.MixinType, visibility);
        }
    }
}