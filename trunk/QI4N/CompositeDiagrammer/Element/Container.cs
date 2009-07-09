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

        [This]
        private Container self;

        public void AddChild(Contained child)
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

        public void RemoveChild(Contained child)
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