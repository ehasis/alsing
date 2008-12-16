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

        public void Mutate(DnaDrawing drawing)
        {
            //if (Tools.WillMutate(Settings.ActiveMovePointMaxMutationRate))
            //{
            //    X = Tools.GetRandomNumber(0, drawing.SourceImage.Width);
            //    Y = Tools.GetRandomNumber(0, drawing.SourceImage.Height);
            //    drawing.SetDirty();
            //}

            //if (Tools.WillMutate(Settings.ActiveMovePointMidMutationRate))
            //{
            //    X =
            //        Math.Min(
            //            Math.Max(0,
            //                     X +
            //                     Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMid,
            //                                           Settings.ActiveMovePointRangeMid)), drawing.SourceImage.Width);
            //    Y =
            //        Math.Min(
            //            Math.Max(0,
            //                     Y +
            //                     Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMid,
            //                                           Settings.ActiveMovePointRangeMid)), drawing.SourceImage.Height);
            //    drawing.SetDirty();
            //}

            if (Tools.WillMutate(Settings.ActiveMovePointMinMutationRate))
            {
                X =
                    Math.Min(
                        Math.Max(0,
                                 X +
                                 Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMin,
                                                       Settings.ActiveMovePointRangeMin)), drawing.SourceImage.Width);
                Y =
                    Math.Min(
                        Math.Max(0,
                                 Y +
                                 Tools.GetRandomNumber(-Settings.ActiveMovePointRangeMin,
                                                       Settings.ActiveMovePointRangeMin)), drawing.SourceImage.Height);
                drawing.SetDirty();
            }
        }
    }
}