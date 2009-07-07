namespace CompositeDiagrammer.Element
{
    using QI4N.Framework;

    public class ElementContainerBehaviorMixin : ElementContainerBehavior
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