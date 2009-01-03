using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using GenArt.Classes;

namespace GenArt.AST
{
    [Serializable]
    internal class DnaDrawing
    {
        internal List<DnaPolygon> Polygons { get; set; }

        [XmlIgnore]
        internal bool IsDirty { get; set; }

        internal void SetDirty()
        {
            IsDirty = true;
        }

        internal void Init(JobInfo info)
        {
            Polygons = new List<DnaPolygon>();

            for (int i = 0; i < info.Settings.PolygonsMin; i++)
                AddPolygon(info);

            SetDirty();
        }

        internal DnaDrawing Clone()
        {
            var drawing = new DnaDrawing
                              {
                                  Polygons = new List<DnaPolygon>(),
                                  //SourceImage = SourceImage,
                              };

            unchecked
            {
                for (int i = 0; i < Polygons.Count; i++)
                {
                    DnaPolygon polygon = Polygons[i];
                    drawing.Polygons.Add(polygon.Clone());
                }
            }
            return drawing;
        }


        internal void Mutate(JobInfo info)
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

                unchecked
                {
                    for (int i = 0; i < Polygons.Count; i++)
                    {
                        DnaPolygon polygon = Polygons[i];
                        polygon.Mutate(this, info);
                    }
                }
            }
        }

        internal void MovePolygon(JobInfo info)
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

        internal void RemovePolygon(JobInfo info)
        {
            if (Polygons.Count > info.Settings.PolygonsMin)
            {
                int index = info.GetRandomNumber(0, Polygons.Count);
                Polygons.RemoveAt(index);
                SetDirty();
            }
        }

        internal void AddPolygon(JobInfo info)
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

        internal void AddPolygonClone(JobInfo info)
        {
            if (Polygons.Count < info.Settings.PolygonsMax)
            {
                if (Polygons.Count < 1)
                    AddPolygon(info);
                else
                {
                    DnaPolygon parent = Polygons[info.GetRandomNumber(0, Polygons.Count)];
                    DnaPolygon newPolygon = parent.Clone();
                    Polygons.Insert(Polygons.IndexOf(parent), newPolygon);

                    newPolygon.Offset(info.GetRandomNumber(-6, 6), info.GetRandomNumber(-6, 6));

                    SetDirty();
                }
            }
        }


        internal DnaDrawing GetMutatedChild(JobInfo info)
        {
            DnaDrawing child = Clone();
            child.Mutate(info);
            return child;
        }
    }
}