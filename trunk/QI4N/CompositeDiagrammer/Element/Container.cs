namespace CompositeDiagrammer
{
    using System.Collections.Generic;

    using QI4N.Framework;

    [Mixins(typeof(ContainerMixin))]
    public interface Container
    {
        void AddChild(IsContainable child);

        void RemoveChild(IsContainable child);

        void RenderChildren(RenderInfo renderInfo);
    }


    public class ContainerMixin : Container
    {
        private readonly IList<IsContainable> children = new List<IsContainable>();

        [This]
        private Container self;

        public void AddChild(IsContainable child)
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

        public void RemoveChild(IsContainable child)
        {
            this.children.Remove(child);
            child.Parent = null;
        }

        public void RenderChildren(RenderInfo renderInfo)
        {
            foreach (Shape child in this.children)
            {
                child.Render(renderInfo);
            }
        }
    }
}