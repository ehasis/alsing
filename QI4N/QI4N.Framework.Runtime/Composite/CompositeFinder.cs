namespace QI4N.Framework.Runtime
{
    public class CompositeFinder : TypeFinder<CompositeModel>
    {
        protected override CompositeModel FindModel(ModuleModel model, Visibility visibility)
        {
            CompositeModel m = CompositeModel.NewModel(Type, null);
            return m;
        }
    }
}