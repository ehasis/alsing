using System.Collections.Generic;

namespace AlbinoHorse.Model
{
    public interface IUmlDiagramData
    {
        T CreateShape<T>() where T : Shape, new();
        void RemoveShape(Shape item);
        IList<Shape> GetShapes();
    }
}