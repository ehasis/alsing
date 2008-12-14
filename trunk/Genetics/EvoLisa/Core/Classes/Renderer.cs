using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using GenArt.AST;

namespace GenArt.Classes
{
    public static class Renderer
    {
        //Render a Drawing
        public static void Render(DnaDrawing drawing, Graphics g, int scale)
        {
            g.Clear(Color.Black);


            foreach (DnaPolygon polygon in drawing.Polygons)
                Render(polygon, g, scale);
        }


        //Render a polygon
        private static void Render(DnaPolygon polygon, Graphics g, int scale)
        {
            Point[] points = GetGdiPoints(polygon.Points, scale);
            using (Brush brush = GetGdiBrush(polygon.Brush))
            {
                g.FillClosedCurve(brush, points, FillMode.Winding);                
            }
        }

        //Convert a list of DnaPoint to a list of System.Drawing.Point's
        private static Point[] GetGdiPoints(IList<DnaPoint> points, int scale)
        {
            var pts = new Point[points.Count];
            int i = 0;
            foreach (DnaPoint pt in points)
            {
                pts[i++] = new Point(pt.X*scale, pt.Y*scale);
            }
            return pts;
        }

        //Convert a DnaBrush to a System.Drawing.Brush
        private static Brush GetGdiBrush(DnaBrush b)
        {
            //var pb = new PathGradientBrush(points);
            //pb.CenterColor = Color.FromArgb(b.Alpha, b.Red, b.Green, b.Blue);
            //pb.SurroundColors = new Color[] {Color.Transparent};

            //return pb;

            return new SolidBrush(Color.FromArgb(b.Alpha, b.Red, b.Green, b.Blue));
        }
    }
}