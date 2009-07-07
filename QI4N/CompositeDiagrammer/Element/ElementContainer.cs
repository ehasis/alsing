namespace CompositeDiagrammer
{
    using System.Collections.Generic;

    using QI4N.Framework;

    [Mixins(typeof(ElementContainerMixin))]
    public interface ElementContainer 
    {
        void AddChild(Element child);

        void RemoveChild(Element child);
    }

    public interface ElementContainerState
    {
        IList<Element> Children { get; set; }
    }
}