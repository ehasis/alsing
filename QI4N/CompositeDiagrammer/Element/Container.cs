namespace CompositeDiagrammer
{
    using System.Collections.Generic;

    using QI4N.Framework;

    [Mixins(typeof(ContainerMixin))]
    public interface Container
    {
        void AddChild(Contained child);

        void RemoveChild(Contained child);

        void RenderChildren(RenderInfo renderInfo);
    }


    public class ContainerMixin : Container
    {
        private readonly IList<Contained> children = new List<Contained>();

        public void AddChild(Contained child)
        {
            this.children.Add(child);
        }

        public void RemoveChild(Contained child)
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