namespace CompositeDiagrammer
{
    using System.Collections.Generic;

    using QI4N.Framework;

    [Mixins(typeof(ElementContainerMixin))]
    public interface ElementContainer 
    {
        void AddChild(ElementComposite child);

        void RemoveChild(ElementComposite child);

        void RenderChildren(RenderInfo renderInfo);
    }

    public interface ElementContainerState
    {
        IList<ElementComposite> Children { get; set; }
    }
}