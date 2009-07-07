namespace CompositeDiagrammer.Element
{
    public interface Rotatable :  RotatableBehavior
    {
    }

    public interface RotatableState
    {
        double Angle { get; set; }
    }

    public interface RotatableBehavior
    {
        void Rotate(double angle);
    }
}