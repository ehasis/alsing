using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using GenArt.AST;

namespace GenArt.Classes
{
    internal static class Renderer
    {
        //Render a Drawing
        internal static void Render(DnaDrawing drawing, Graphics g, int scale)
        {
            for (int i = 0; i < drawing.Polygons.Count;i++ )
            {
                DnaPolygon polygon = drawing.Polygons[i];
                Render(polygon, g, scale);
            }
        }


        //Render a polygon
        private static void Render(DnaPolygon polygon, Graphics g, int scale)
        {
            Point[] points = GetGdiPoints(polygon.Points, scale);
            using (Brush brush = GetGdiBrush(polygon.Brush))
            {
                g.FillPolygon(brush, points, FillMode.Winding);
                //g.FillClosedCurve(brush, points, FillMode.Winding);
            }
        }



        private static Point[] GetGdiPoints(IList<DnaPoint> points, int scale)
        {
            var pts = new Point[points.Count];

            unchecked
            {
                for (int i = 0; i < points.Count; i++)
                {
                    var pt = points[i];
                    pts[i] = new Point(pt.X * scale, pt.Y * scale);
                }
            }
            return pts;
        }

        //Convert a DnaBrush to a System.Drawing.Brush
        private static Brush GetGdiBrush(DnaBrush b)
        {
            return new SolidBrush(Color.FromArgb(b.Alpha, b.Red, b.Green, b.Blue));
        }
    }
}