namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public class RotatableMixin : Rotatable
    {
        [This]
        private RotatableState state;

        public void Rotate(double angle)
        {
            this.state.Angle = angle;
        }
    }
}