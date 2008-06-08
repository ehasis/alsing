using System.Windows.Forms;
using AlbinoHorse.Windows.Forms;

namespace AlbinoHorse.Infrastructure
{
    public class ShapeKeyEventArgs
    {
        public Keys Key;
        public bool Redraw { get; set; }
        public UmlDesigner Sender { get; set; }
        public int GridSize { get; set; }
        public bool SnapToGrid { get; set; }
    }
}