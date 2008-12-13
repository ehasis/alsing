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

        public void Init(DnaDrawing drawing)
        {
            Points = new List<DnaPoint>();

            //int count = Tools.GetRandomNumber(3, 3);
            var origin = new DnaPoint();
            origin.Init(drawing);

            for (int i = 0; i < Settings.ActivePointsPerPolygonMin; i++)
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
            Brush.Init();
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

        public void Mutate(DnaDrawing drawing)
        {
            if (Tools.WillMutate(Settings.ActiveAddPointMutationRate))
                AddPoint(drawing);

            if (Tools.WillMutate(Settings.ActiveRemovePointMutationRate))
                RemovePoint(drawing);

            Brush.Mutate(drawing);
            Points.ForEach(p => p.Mutate(drawing));
        }

        private void RemovePoint(DnaDrawing drawing)
        {
            if (Points.Count > Settings.ActivePointsPerPolygonMin)
            {
                if (drawing.PointCount > Settings.ActivePointsMin)
                {
                    int index = Tools.GetRandomNumber(0, Points.Count);
                    Points.RemoveAt(index);

                    drawing.SetDirty();
                }
            }
        }

        private void AddPoint(DnaDrawing drawing)
        {
            if (Points.Count < Settings.ActivePointsPerPolygonMax)
            {
                if (drawing.PointCount < Settings.ActivePointsMax)
                {
                    var newPoint = new DnaPoint();

                    int index = Tools.GetRandomNumber(1, Points.Count - 1);

                    DnaPoint prev = Points[index - 1];
                    DnaPoint next = Points[index];

                    newPoint.X = (prev.X + next.X)/2;
                    newPoint.Y = (prev.Y + next.Y)/2;


                    Points.Insert(index, newPoint);

                    drawing.SetDirty();
                }
            }
        }
    }
}