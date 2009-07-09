namespace CompositeDiagrammer
{
    using System.Collections.Generic;

    using QI4N.Framework;

    public interface DrawingService : Drawing, ServiceComposite
    {
    }

    [Mixins(typeof(DrawingMixin))]
    public interface Drawing
    {
        T Create<T>() where T : Element;

        void Remove(Element element);

        GroupShape Group(params Containable[] containables);
    }

    public class DrawingMixin : Drawing
    {
        private readonly IList<Element> elements = new List<Element>();

        [Structure]
        private Module module;

        public T Create<T>() where T : Element
        {
            var element = this.module.TransientBuilderFactory.NewTransient<T>();
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
    }
}