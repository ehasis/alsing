namespace CompositeDiagrammer.Element
{
    using System.Collections.Generic;

    using QI4N.Framework;

    public interface ElementContainer : ElementContainerBehavior
    {
    }

    public interface ElementContainerState
    {
        IList<Element> Children { get; set; }
    }

    [Mixins(typeof(ElementContainerBehaviorMixin))]
    public interface ElementContainerBehavior
    {
        void AddChild(Element child);

        void RemoveChild(Element child);
    }
}