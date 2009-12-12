using System;
using System.Collections.Generic;
using System.Text;

namespace GenArt.Core.Classes
{
    public class DrawingSettings
    {
        public int MaxPolygons { get; set; }
        public int MaxPointsPerPolygon { get; set; }
        public int MutationRateLow { get; set; }
        public int MutationRateHigh { get; set; }

        public DrawingSettings()
        {
            MaxPolygons = 150;
            MaxPointsPerPolygon = 50;
            MutationRateLow = 1500;
            MutationRateHigh = 700;
        }
    }
}
