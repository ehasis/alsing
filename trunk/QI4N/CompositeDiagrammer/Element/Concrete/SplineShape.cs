namespace CompositeDiagrammer
{
    using QI4N.Framework;

    [Mixins(typeof(SplinePathMixin))]
    [Mixins(typeof(SplineNodesMixin))]
    public interface SplineShape : SegmentedShapeComposite, HasLineStyle, IsContainable, IsSelectable
    {
    }

    public class SplineNodesMixin : AbstractNodesMixin
    {
        protected override void InitNodes()
        {
            //node1
            this.nodes.Add(new SegmentedShapeNode());
            //node2
            this.nodes.Add(new SegmentedShapeNode());
            //node3
            this.nodes.Add(new SegmentedShapeNode());
        }
    }
}