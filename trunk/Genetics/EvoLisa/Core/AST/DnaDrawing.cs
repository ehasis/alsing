using System.Collections.Generic;
using System.Xml.Serialization;
using GenArt.Classes;
using System;
using GenArt.Core.Classes;

namespace GenArt.AST
{
    [Serializable]
    public class DnaDrawing
    {
        public List<DnaPolygon> Polygons { get; set; }
        [XmlIgnore]
        public SourceImage SourceImage { get; set; }        

        [XmlIgnore]
        private bool IsDirty { get; set; }

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

        public void Init(Settings settings)
        {
            Polygons = new List<DnaPolygon>();

            for (int i = 0; i < settings.PolygonsMin; i++)
                AddPolygon(settings);

            SetDirty();
        }

        public DnaDrawing Clone()
        {
            var drawing = new DnaDrawing
                              {
                                  Polygons = new List<DnaPolygon>(),
                                  SourceImage = SourceImage,
                              };
            foreach (DnaPolygon polygon in Polygons)
                drawing.Polygons.Add(polygon.Clone());
            return drawing;
        }


        public void Mutate(Settings settings)
        {
            //mutate always cause atleast one new mutation
            while (!IsDirty)
            {
                if (Tools.WillMutate(settings.AddPolygonMutationRate))
                    AddPolygon(settings);

                if (Tools.WillMutate(settings.RemovePolygonMutationRate))
                    RemovePolygon(settings);

                if (Tools.WillMutate(settings.MovePolygonMutationRate))
                    MovePolygon(settings);

                foreach (DnaPolygon polygon in Polygons)
                    polygon.Mutate(this, settings);
            }
        }

        public void MovePolygon(Settings settings)
        {
            if (Polygons.Count < 1)
                return;

            int index = Tools.GetRandomNumber(0, Polygons.Count);
            DnaPolygon poly = Polygons[index];
            Polygons.RemoveAt(index);
            index = Tools.GetRandomNumber(0, Polygons.Count);
            Polygons.Insert(index, poly);
            SetDirty();
        }

        public void RemovePolygon(Settings settings)
        {
            if (Polygons.Count > settings.PolygonsMin)
            {
                int index = Tools.GetRandomNumber(0, Polygons.Count);
                Polygons.RemoveAt(index);
                SetDirty();
            }
        }

        public void AddPolygon(Settings settings)
        {
            if (Polygons.Count < settings.PolygonsMax)
            {
                var newPolygon = new DnaPolygon();
                newPolygon.Init(this, settings);

                int index = Tools.GetRandomNumber(0, Polygons.Count);

                Polygons.Insert(index, newPolygon);

                SetDirty();
            }
        }

        public void AddPolygonClone(Settings settings)
        {
            if (Polygons.Count < settings.PolygonsMax)
            {
                if (Polygons.Count < 1)
                    AddPolygon(settings);
                else
                {
                    DnaPolygon parent = Polygons[Tools.GetRandomNumber(0, Polygons.Count)];
                    var newPolygon = parent.Clone();
                    Polygons.Insert(Polygons.IndexOf(parent), newPolygon);
                    SetDirty();
                }
            }
        }

    }
}