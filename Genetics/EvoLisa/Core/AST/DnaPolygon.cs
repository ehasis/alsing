using System;
using System.Collections.Generic;
using GenArt.Classes;

namespace GenArt.AST
{
    [Serializable]
    public class DnaPolygon
    {
        public List<DnaPoint> Points { get; set; }
        public DnaBrush Brush { get; set; }

        public int Width { get; set; }
        public bool Splines { get; set; }
        public bool Filled { get; set; }
        public float Tension { get; set; }

        public void Init(DnaDrawing drawing, Settings settings)
        {
            Points = new List<DnaPoint>();

            //int count = Tools.GetRandomNumber(3, 3);
            var origin = new DnaPoint();
            origin.Init(drawing);

            for (int i = 0; i < settings.PointsPerPolygonMin; i++)
            {
                var point = new DnaPoint
                                {
                                    X =
                                        Math.Min(Math.Max(0, origin.X + Tools.GetRandomNumber(-3, 3)),
                                                 drawing.SourceImage.Width),
                                    Y =
                                        Math.Min(Math.Max(0, origin.Y + Tools.GetRandomNumber(-3, 3)),
                                                 drawing.SourceImage.Height)
                                };

                Points.Add(point);
            }

            Brush = new DnaBrush();
            Brush.Init(settings);
        }

        public DnaPolygon Clone()
        {
            var newPolygon = new DnaPolygon
                                 {
                                     Points = new List<DnaPoint>(),
                                     Brush = Brush.Clone()
                                 };
            foreach (DnaPoint point in Points)
                newPolygon.Points.Add(point.Clone());

            return newPolygon;
        }

        public bool IsComplex { get; set; }

        public void Mutate(DnaDrawing drawing, Settings settings)
        {
            if (Tools.WillMutate(settings.AddPointMutationRate))
                AddPoint(drawing, settings);

            if (Tools.WillMutate(settings.RemovePointMutationRate))
                RemovePoint(drawing, settings);

            Brush.Mutate(drawing, settings);
            Points.ForEach(p => p.Mutate(drawing, settings));

            IsComplex = false;// checkComplex();
        }

        private void RemovePoint(DnaDrawing drawing, Settings settings)
        {
            if (Points.Count > settings.PointsPerPolygonMin)
            {
                if (drawing.PointCount > settings.PointsMin)
                {
                    int index = Tools.GetRandomNumber(0, Points.Count);
                    Points.RemoveAt(index);

                    drawing.SetDirty();
                }
            }
        }

        private void AddPoint(DnaDrawing drawing, Settings settings)
        {
            if (Points.Count < settings.PointsPerPolygonMax)
            {
                if (drawing.PointCount < settings.PointsMax)
                {
                    var newPoint = new DnaPoint();

                    int index = Tools.GetRandomNumber(1, Points.Count - 1);

                    DnaPoint prev = Points[index - 1];
                    DnaPoint next = Points[index];

                    newPoint.X = (prev.X + next.X) / 2;
                    newPoint.Y = (prev.Y + next.Y) / 2;


                    Points.Insert(index, newPoint);

                    drawing.SetDirty();
                }
            }
        }

        //smeck, funkar ju inte
        public bool checkComplex()
        {
            int i = 0, j;
            for (j = i + 2; j < Points.Count - 1; j++)
                if (intersect(i, j))
                    return true;
            for (i = 1; i < Points.Count; i++)
                for (j = i + 2; j < Points.Count; j++)
                    if (intersect(i, j))
                        return true;
            return false;
        }

        public bool intersect(int i1, int i2)
        {
            int s1 = (i1 > 0) ? i1 - 1 : Points.Count - 1;
            int s2 = (i2 > 0) ? i2 - 1 : Points.Count - 1;
            return ccw(Points[s1], Points[i1], Points[s2])
                != ccw(Points[s1], Points[i1], Points[i2])
                && ccw(Points[s2], Points[i2], Points[s1])
                != ccw(Points[s2], Points[i2], Points[i1]);
        }

        public bool ccw(DnaPoint p1, DnaPoint p2, DnaPoint p3)
        {
            double dx1 = p2.X - p1.X;
            double dy1 = p2.Y - p1.Y;
            double dx2 = p3.X - p2.X;
            double dy2 = p3.Y - p2.Y;
            return dy1 * dx2 < dy2 * dx1;
        }

    }
}