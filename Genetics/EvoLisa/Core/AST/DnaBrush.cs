using GenArt.Classes;
using System;

namespace GenArt.AST
{
    [Serializable]
    public class DnaBrush
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Alpha { get; set; }

        public void Init(JobInfo info)
        {
            Red = info.GetRandomNumber(info.Settings.RedRangeMin, info.Settings.RedRangeMax);
            Green = info.GetRandomNumber(info.Settings.GreenRangeMin, info.Settings.GreenRangeMax);
            Blue = info.GetRandomNumber(info.Settings.BlueRangeMin, info.Settings.BlueRangeMax);
            //Alpha = info.GetRandomNumber(settings.AlphaRangeMin, settings.AlphaRangeMax);
            Alpha = info.GetRandomNumber(10, 60);
        }

        public DnaBrush Clone()
        {
            return new DnaBrush
                       {
                           Alpha = Alpha,
                           Blue = Blue,
                           Green = Green,
                           Red = Red,
                       };
        }

        public void Mutate(DnaDrawing drawing, JobInfo info)
        {
            if (info.WillMutate(info.Settings.ColorMutationRate))
            {
                Red = info.GetRandomNumber(info.Settings.RedRangeMin, info.Settings.RedRangeMax);

                drawing.SetDirty();
            }

            if (info.WillMutate(info.Settings.ColorMutationRate))
            {
                Green = info.GetRandomNumber(info.Settings.GreenRangeMin, info.Settings.GreenRangeMax);

                drawing.SetDirty();
            }
            if (info.WillMutate(info.Settings.ColorMutationRate))
            {
                Blue = info.GetRandomNumber(info.Settings.BlueRangeMin, info.Settings.BlueRangeMax);

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
