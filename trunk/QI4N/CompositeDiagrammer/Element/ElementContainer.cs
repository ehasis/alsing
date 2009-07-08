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

    public class ElementContainerMixin : ElementContainer
    {
        [This]
        private ElementContainerState state;

        public void AddChild(ElementComposite child)
        {
            this.state.Children.Add(child);
        }

        public void RemoveChild(ElementComposite child)
        {
            this.state.Children.Remove(child);
        }

        public void RenderChildren(RenderInfo renderInfo)
        {
            foreach (ElementComposite child in this.state.Children)
            {
                child.Render(renderInfo);
            }
        }
    }
}