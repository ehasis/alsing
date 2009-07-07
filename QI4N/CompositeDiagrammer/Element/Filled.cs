namespace CompositeDiagrammer
{
    using QI4N.Framework;

    [Mixins(typeof(FilledMixin))]
    public interface Filled
    {
        void RenderFilling(RenderInfo renderInfo);
    }
}