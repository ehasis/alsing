using System;
using GenArt.Classes;
using GenArt.Core.Classes;

namespace GenArt.AST
{
    [Serializable]
    public class DnaPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Init(DnaDrawing drawing)
        {
            X = Tools.GetRandomNumber(0, drawing.SourceImage.Width);
            Y = Tools.GetRandomNumber(0, drawing.SourceImage.Height);
        }

        public DnaPoint Clone()
        {
            return new DnaPoint
                       {
                           X = X,
                           Y = Y,
                       };
        }

        public void Mutate(DnaDrawing drawing, Settings settings)
        {
            if (Tools.WillMutate(settings.MovePointMaxMutationRate))
            {
                X = Tools.GetRandomNumber(0, drawing.SourceImage.Width);
                Y = Tools.GetRandomNumber(0, drawing.SourceImage.Height);
                drawing.SetDirty();
            }

            if (Tools.WillMutate(settings.MovePointMidMutationRate))
            {
                X =
                    Math.Min(
                        Math.Max(0,
                                 X +
                                 Tools.GetRandomNumber(-settings.MovePointRangeMid,
                                                       settings.MovePointRangeMid)), drawing.SourceImage.Width);
                Y =
                    Math.Min(
                        Math.Max(0,
                                 Y +
                                 Tools.GetRandomNumber(-settings.MovePointRangeMid,
                                                       settings.MovePointRangeMid)), drawing.SourceImage.Height);
                drawing.SetDirty();
            }

            if (Tools.WillMutate(settings.MovePointMinMutationRate))
            {
                X =
                    Math.Min(
                        Math.Max(0,
                                 X +
                                 Tools.GetRandomNumber(-settings.MovePointRangeMin,
                                                       settings.MovePointRangeMin)), drawing.SourceImage.Width);
                Y =
                    Math.Min(
                        Math.Max(0,
                                 Y +
                                 Tools.GetRandomNumber(-settings.MovePointRangeMin,
                                                       settings.MovePointRangeMin)), drawing.SourceImage.Height);
                drawing.SetDirty();
            }
        }
    }
}