namespace CompositeDiagrammer
{
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
    }
}