using System.Collections.Generic;

namespace AlbinoHorse.Model
{
    public class DefaultUmlDiagramData : IUmlDiagramData
    {
        public DefaultUmlDiagramData()
        {
            Shapes = new List<Shape>();
        }

        public List<Shape> Shapes { get; set; }

        #region IUmlDiagramData Members

        public T CreateShape<T>() where T : Shape, new()
        {
            var shape = new T();
            Shapes.Add(shape);
            return shape;
        }

        public void RemoveShape(Shape item)
        {
            Shapes.Remove(item);
        }

        public IList<Shape> GetShapes()
        {
            return Shapes;
        }

        #endregion
    }
}