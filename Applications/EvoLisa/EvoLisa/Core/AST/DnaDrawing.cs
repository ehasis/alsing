using System.Collections.Generic;
using System.Xml.Serialization;
using GenArt.Classes;
using System;
using GenArt.Core.Classes;
using System.Drawing;

namespace GenArt.AST
{
    [Serializable]
    public class DnaDrawing
    {
        public List<DnaPolygon> Polygons { get; set; }
        //[XmlIgnore]
        //public SourceImage SourceImage { get; set; }


        [XmlIgnore]
        public bool IsDirty { get; set; }

        public int PointCount
        {
            get
            {
                int pointCount = 0;
                foreach (DnaPolygon polygon in Polygons)
                    pointCount += polygon.Points.Count;

                return pointCount;
            }
        }

        public void SetDirty()
        {
            IsDirty = true;
        }

        public void Init(JobInfo info)
        {
            Polygons = new List<DnaPolygon>();

            for (int i = 0; i < info.Settings.PolygonsMin; i++)
                AddPolygon(info);

            SetDirty();
        }

        public DnaDrawing Clone()
        {
            var drawing = new DnaDrawing
                              {
                                  Polygons = new List<DnaPolygon>(),
                                  //SourceImage = SourceImage,
                              };
            foreach (DnaPolygon polygon in Polygons)
                drawing.Polygons.Add(polygon.Clone());
            return drawing;
        }


        public void Mutate(JobInfo info)
        {
            IsDirty = false;
            while (!IsDirty)
            {
                if (!info.Settings.MuteAddPolygonNew)
                    if (info.WillMutate(info.Settings.AddPolygonMutationRate))
                        AddPolygon(info);

                if (!info.Settings.MuteAddPolygonClone)
                    if (info.WillMutate(info.Settings.AddPolygonCloneMutationRate))
                        AddPolygonClone(info);

                if (!info.Settings.MuteRemovePolygon)
                    if (info.WillMutate(info.Settings.RemovePolygonMutationRate))
                        RemovePolygon(info);

                if (!info.Settings.MuteMovePolygon)
                    if (info.WillMutate(info.Settings.MovePolygonMutationRate))
                        MovePolygon(info);

                foreach (DnaPolygon polygon in Polygons)
                    polygon.Mutate(this, info);
            }
        }

        public void MovePolygon(JobInfo info)
        {
            if (Polygons.Count < 1)
                return;

            int index = info.GetRandomNumber(0, Polygons.Count);
            DnaPolygon poly = Polygons[index];
            Polygons.RemoveAt(index);
            index = info.GetRandomNumber(0, Polygons.Count);
            Polygons.Insert(index, poly);
            SetDirty();
        }

        public void RemovePolygon(JobInfo info)
        {
            if (Polygons.Count > info.Settings.PolygonsMin)
            {
                int index = info.GetRandomNumber(0, Polygons.Count);
                Polygons.RemoveAt(index);
                SetDirty();
            }
        }

        public void AddPolygon(JobInfo info)
        {
            if (Polygons.Count < info.Settings.PolygonsMax)
            {
                var newPolygon = new DnaPolygon();
                newPolygon.Init(this, info);

                int index = info.GetRandomNumber(0, Polygons.Count);

                Polygons.Insert(index, newPolygon);

                SetDirty();
            }
        }

        public void AddPolygonClone(JobInfo info)
        {
            if (Polygons.Count < info.Settings.PolygonsMax)
            {
                if (Polygons.Count < 1)
                    AddPolygon(info);
                else
                {
                    DnaPolygon parent = Polygons[info.GetRandomNumber(0, Polygons.Count)];
                    var newPolygon = parent.Clone();
                    Polygons.Insert(Polygons.IndexOf(parent), newPolygon);

                    newPolygon.Offset(info.GetRandomNumber(-6, 6), info.GetRandomNumber(-6, 6));

                    newPolygon.Width = info.GetRandomNumber(1, 8);

                    SetDirty();
                }
            }
        }

    }
}