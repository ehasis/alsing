using System.Collections.Generic;
using System.Drawing;

namespace AlbinoHorse.Infrastructure
{
    public class RenderInfo
    {
        public RenderInfo()
        {
            BoundingItems = new List<BoundingItem>();
        }

        public Graphics Graphics { get; set; }
        public Rectangle VisualBounds { get; set; }
        public List<BoundingItem> BoundingItems { get; set; }
        public int GridSize { get; set; }
        public bool ShowGrid { get; set; }
        public double Zoom { get; set; }
        //public Size ReturnedSize { get; set; }
        public Rectangle ReturnedBounds { get; set; }
        public bool Preview { get; set; }
    }
}