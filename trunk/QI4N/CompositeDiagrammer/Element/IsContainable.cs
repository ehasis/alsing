namespace CompositeDiagrammer
{
    using QI4N.Framework;

    [Mixins(typeof(ContainableMixin))]
    public interface IsContainable
    {
        Container Parent { get; set; }
    }

    public class ContainableMixin : IsContainable
    {
        [This]
        private IsContainable self;

        public Container Parent { get; set; }
    }
}