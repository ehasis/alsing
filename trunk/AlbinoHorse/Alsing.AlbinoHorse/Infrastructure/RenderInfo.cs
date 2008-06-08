using System.Collections.Generic;
using System.Drawing;

namespace AlbinoHorse.Infrastructure
{
    public class RenderInfo
    {
        public RenderInfo()
        {
            BoundingBoxes = new List<BoundingBox>();
        }

        public Graphics Graphics { get; set; }
        public Rectangle VisualBounds { get; set; }
        public List<BoundingBox> BoundingBoxes { get; set; }
        public int GridSize { get; set; }
        public bool ShowGrid { get; set; }
        public double Zoom { get; set; }
        //public Size ReturnedSize { get; set; }
        public Rectangle ReturnedBounds { get; set; }
        public bool Preview { get; set; }
    }
}