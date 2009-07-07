namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public class ElementContainerMixin : ElementContainer
    {
        [This]
        private ElementContainerState state;

        public void AddChild(Element child)
        {
            this.state.Children.Add(child);
        }

        public void RemoveChild(Element child)
        {
            this.state.Children.Remove(child);
        }
    }
}