using GenArt.Classes;
using System;

namespace GenArt.AST
{
    [Serializable]
    internal class DnaBrush
    {
        internal int Red { get; set; }
        internal int Green { get; set; }
        internal int Blue { get; set; }
        internal int Alpha { get; set; }

        internal void Init(JobInfo info)
        {
            Red = info.GetRandomNumber(0, 255);
            Green = info.GetRandomNumber(0, 255);
            Blue = info.GetRandomNumber(0, 255);
            Alpha = info.GetRandomNumber(10, 60);
        }

        internal DnaBrush Clone()
        {
            return new DnaBrush
                       {
                           Alpha = Alpha,
                           Blue = Blue,
                           Green = Green,
                           Red = Red,
                       };
        }

        internal void Mutate(DnaDrawing drawing, JobInfo info)
        {
            if (info.WillMutate(info.Settings.ColorMutationRate))
            {
                Red = info.GetRandomNumber(0, 255);

                drawing.SetDirty();
            }

            if (info.WillMutate(info.Settings.ColorMutationRate))
            {
                Green = info.GetRandomNumber(0, 255);

                drawing.SetDirty();
            }
            if (info.WillMutate(info.Settings.ColorMutationRate))
            {
                Blue = info.GetRandomNumber(0, 255);

                drawing.SetDirty();
            }

            if (info.WillMutate(info.Settings.ColorMutationRate))
            {
                Alpha = info.GetRandomNumber(info.Settings.AlphaRangeMin, info.Settings.AlphaRangeMax);

                drawing.SetDirty();
            }
        }
    }
}
