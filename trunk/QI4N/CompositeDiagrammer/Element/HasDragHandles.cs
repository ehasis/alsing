using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompositeDiagrammer
{
    public interface HasDragHandles
    {
        IEnumerable<DragHandle> GetDragHandles();
    }

    public class DragHandle
    {
        public int X { get; set; }

        public int Y { get; set; }
    }
}
