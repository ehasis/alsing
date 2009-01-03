using System;
using System.Collections.Generic;
using GenArt.Classes;

namespace GenArt.AST
{
    [Serializable]
    internal class DnaPolygon
    {
        internal List<DnaPoint> Points { get; set; }
        internal DnaBrush Brush { get; set; }

        internal void Init(DnaDrawing drawing, JobInfo info)
        {
            Points = new List<DnaPoint>();

            //int count = info.GetRandomNumber(3, 3);
            var origin = new DnaPoint();
            origin.Init(drawing, info);

            if (drawing.Polygons.Count < 1)
            {
                origin.X = info.SourceImage.Width / 2;
                origin.Y = info.SourceImage.Height / 2;
            }

            for (int i = 0; i < info.Settings.PointsPerPolygonMin; i++)
            {
                var point = new DnaPoint
                                {
                                    X =
                                        Math.Min(Math.Max(0, origin.X + info.GetRandomNumber(-3, 3)),
                                                 info.SourceImage.Width),
                                    Y =
                                        Math.Min(Math.Max(0, origin.Y + info.GetRandomNumber(-3, 3)),
                                                 info.SourceImage.Height)
                                };

                Points.Add(point);
            }

            Brush = new DnaBrush();
            Brush.Init(info);
        }


        internal DnaPolygon Clone()
        {
            var newPolygon = new DnaPolygon
                                 {
                                     Points = new List<DnaPoint>(),
                                     Brush = Brush.Clone()
                                 };

            unchecked
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    DnaPoint point = Points[i];
                    newPolygon.Points.Add(point.Clone());
                }
            }

            return newPolygon;
        }

        internal bool IsComplex { get; set; }

        internal void Mutate(DnaDrawing drawing, JobInfo info)
        {
            if (info.WillMutate(info.Settings.AddPointMutationRate))
                AddPoint(drawing, info);

            if (info.WillMutate(info.Settings.RemovePointMutationRate))
                RemovePoint(drawing, info);

            Brush.Mutate(drawing, info);

            unchecked
            {
                for (int i = 0; i < Points.Count;i++)
                {
                    var point = Points[i];
                    point.Mutate(drawing, info);
                }
            }
        }

        private void RemovePoint(DnaDrawing drawing, JobInfo info)
        {
            if (Points.Count > info.Settings.PointsPerPolygonMin)
            {

                int index = info.GetRandomNumber(0, Points.Count);
                Points.RemoveAt(index);

                drawing.SetDirty();

            }
        }

        private void AddPoint(DnaDrawing drawing, JobInfo info)
        {
            if (Points.Count < info.Settings.PointsPerPolygonMax)
            {

                var newPoint = new DnaPoint();

                int index = info.GetRandomNumber(1, Points.Count - 1);

                DnaPoint prev = Points[index - 1];
                DnaPoint next = Points[index];

                newPoint.X = (prev.X + next.X)/2;
                newPoint.Y = (prev.Y + next.Y)/2;


                Points.Insert(index, newPoint);

                drawing.SetDirty();

            }
        }

        ////smeck, funkar ju inte
        //internal bool checkComplex()
        //{
        //    int i = 0, j;
        //    for (j = i + 2; j < Points.Count - 1; j++)
        //        if (intersect(i, j))
        //            return true;
        //    for (i = 1; i < Points.Count; i++)
        //        for (j = i + 2; j < Points.Count; j++)
        //            if (intersect(i, j))
        //                return true;
        //    return false;
        //}

        //internal bool intersect(int i1, int i2)
        //{
        //    int s1 = (i1 > 0) ? i1 - 1 : Points.Count - 1;
        //    int s2 = (i2 > 0) ? i2 - 1 : Points.Count - 1;
        //    return ccw(Points[s1], Points[i1], Points[s2])
        //        != ccw(Points[s1], Points[i1], Points[i2])
        //        && ccw(Points[s2], Points[i2], Points[s1])
        //        != ccw(Points[s2], Points[i2], Points[i1]);
        //}

        //internal bool ccw(DnaPoint p1, DnaPoint p2, DnaPoint p3)
        //{
        //    double dx1 = p2.X - p1.X;
        //    double dy1 = p2.Y - p1.Y;
        //    double dx2 = p3.X - p2.X;
        //    double dy2 = p3.Y - p2.Y;
        //    return dy1 * dx2 < dy2 * dx1;
        //}


        internal void Offset(int x, int y)
        {
            foreach (DnaPoint point in Points)
            {
                point.X += x;
                point.Y += y;
            }
        }

        public override string ToString()
        {
            string points = "";
            foreach (var point in Points)
            {
                points += point + " ";
            }

            return string.Format("Polygon({0})", points);
        }
    }
}