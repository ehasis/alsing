namespace CompositeDiagrammer
{
    using QI4N.Framework;

    [Mixins(typeof(ContainableMixin))]
    public interface Containable
    {
        Container Parent { get; set; }
    }

    public class ContainableMixin : Containable
    {
        [This]
        private Containable self;

        public Container Parent { get; set; }
    }
}