namespace CompositeDiagrammer
{
    using System.Collections.Generic;

    using QI4N.Framework;

    public interface DrawingService : Drawing, TransientComposite
    {
    }

    [Mixins(typeof(DrawingMixin))]
    public interface Drawing
    {
        T Create<T>() where T : Element;

        void Remove(Element element);

        GroupShape Group(params Containable[] containables);

        string SayHello();
    }

    public class DrawingMixin : Drawing
    {
        private readonly IList<Element> elements = new List<Element>();

        [Structure]
        private TransientBuilderFactory tbf;

        public T Create<T>() where T : Element
        {
            var element = this.tbf.NewTransient<T>();
            this.elements.Add(element);
            return element;
        }

        public GroupShape Group(params Containable[] containables)
        {
            var group = this.Create<GroupShape>();
            foreach (Containable containable in containables)
            {
                group.AddChild(containable);
            }

            return group;
        }

        public void Remove(Element element)
        {
            this.elements.Remove(element);
        }

        public string SayHello()
        {
            return "hej";
        }
    }
}