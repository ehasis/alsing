namespace CompositeDiagrammer
{
    using QI4N.Framework;

    [Mixins(typeof(LinePathMixin))]
    [Mixins(typeof(LineNodesMixin))]
    public interface LineShape : SegmentedShapeComposite, HasLineStyle, IsContainable, IsSelectable
    {
    }

    public class LineNodesMixin : AbstractNodesMixin
    {
        protected override void InitNodes()
        {
            //node1
            this.nodes.Add(new SegmentedShapeNode());
            //node2
            this.nodes.Add(new SegmentedShapeNode());
        }
    }
}