using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompositeDiagrammer
{
    public interface LineShape : SegmentedShapeComposite, Containable, Selectable
    {

    }
}
