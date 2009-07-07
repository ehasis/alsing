namespace CompositeDiagrammer
{
    using QI4N.Framework;

    [Mixins(typeof(RotatableMixin))]
    public interface Rotatable
    {
    }

    public interface RotatableState
    {
        double Angle { get; set; }
    }
}