namespace CompositeDiagrammer
{
    using System.Collections.Generic;

    using QI4N.Framework;

    [Mixins(typeof(ContainerMixin))]
    public interface Container
    {
        void AddChild(Containable child);

        void RemoveChild(Containable child);

        void RenderChildren(RenderInfo renderInfo);
    }


    public class ContainerMixin : Container
    {
        private readonly IList<Containable> children = new List<Containable>();

        [This]
        private Container self;

        public void AddChild(Containable child)
        {
            if (child.Parent == self)
                return;

            if (child.Parent != null)
            {
                child.Parent.RemoveChild(child);
            }

            this.children.Add(child);
            child.Parent = self;
        }

        public void RemoveChild(Containable child)
        {
            this.children.Remove(child);
            child.Parent = null;
        }

        public void RenderChildren(RenderInfo renderInfo)
        {
            foreach (Element child in this.children)
            {
                child.Render(renderInfo);
            }
        }
    }
}