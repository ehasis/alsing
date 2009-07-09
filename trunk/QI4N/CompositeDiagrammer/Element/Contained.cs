namespace CompositeDiagrammer
{
    using QI4N.Framework;

    [Mixins(typeof(ContainedMixin))]
    public interface Contained
    {
        Container Parent { get; set; }
    }

    public class ContainedMixin : Contained
    {
        private Container parent;

        [This]
        private Contained self;

        public Container Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
            }
        }
    }
}