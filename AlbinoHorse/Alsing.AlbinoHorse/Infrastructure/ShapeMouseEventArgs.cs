using System.Windows.Forms;
using AlbinoHorse.Windows.Forms;

namespace AlbinoHorse.Infrastructure
{
    public class ShapeMouseEventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
        public MouseButtons Button { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public bool Redraw { get; set; }
        public UmlDesigner Sender { get; set; }
        public int GridSize { get; set; }
        public bool SnapToGrid { get; set; }
    }
}