namespace CompositeDiagrammer.Element
{
    using QI4N.Framework;

    public interface Rotatable :  RotatableBehavior
    {
    }

    public interface RotatableState
    {
        double Angle { get; set; }
    }

    [Mixins(typeof(RotatableBehaviorMixin))]
    public interface RotatableBehavior
    {
        void Rotate(double angle);
    }
}