namespace CompositeDiagrammer.Element
{
    using QI4N.Framework;

    public class RotatableBehaviorMixin : RotatableBehavior
    {
        [This]
        private RotatableState state;

        public void Rotate(double angle)
        {
            this.state.Angle = angle;
        }
    }
}