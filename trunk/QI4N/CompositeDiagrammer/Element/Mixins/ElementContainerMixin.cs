namespace CompositeDiagrammer
{
    using System;

    using QI4N.Framework;

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
            
            foreach(ElementComposite child in state.Children)
            {
                child.Render(renderInfo);
            }
        }
    }
}