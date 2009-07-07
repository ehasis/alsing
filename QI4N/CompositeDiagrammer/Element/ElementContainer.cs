namespace CompositeDiagrammer.Element
{
    using System.Collections.Generic;

    public interface ElementContainer : ElementContainerBehavior
    {
    }

    public interface ElementContainerState
    {
        IList<Element> Children { get; set; }
    }

    public interface ElementContainerBehavior
    {
        void AddChild(Element child);

        void RemoveChild(Element child);
    }
}