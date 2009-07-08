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


    public class ElementContainerMixin : ElementContainer
    {
        private readonly IList<ElementComposite> children = new List<ElementComposite>();

        public void AddChild(ElementComposite child)
        {
            this.children.Add(child);
        }

        public void RemoveChild(ElementComposite child)
        {
            this.children.Remove(child);
        }

        public void RenderChildren(RenderInfo renderInfo)
        {
            foreach (ElementComposite child in this.children)
            {
                child.Render(renderInfo);
            }
        }
    }
}