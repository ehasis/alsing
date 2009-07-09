using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompositeDiagrammer
{
    using QI4N.Framework;

    public interface Element1DComposite : Element2D, ElementComposite
    {
    }

    [Mixins(typeof(Element1DMixin))]
    public interface Element1D
    {
        void Move(int offsetX, int offsetY);

        void Rotate(double angle);
    }

    public class Element1DMixin
    {
    }
}
