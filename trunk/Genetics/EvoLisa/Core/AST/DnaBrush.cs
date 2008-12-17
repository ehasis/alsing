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
            Red = Tools.GetRandomNumber(info.Settings.RedRangeMin, info.Settings.RedRangeMax);
            Green = Tools.GetRandomNumber(info.Settings.GreenRangeMin, info.Settings.GreenRangeMax);
            Blue = Tools.GetRandomNumber(info.Settings.BlueRangeMin, info.Settings.BlueRangeMax);
            //Alpha = Tools.GetRandomNumber(settings.AlphaRangeMin, settings.AlphaRangeMax);
            Alpha = Tools.GetRandomNumber(10, 60);
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
            if (Tools.WillMutate(info.Settings.ColorMutationRate))
            {
                Red = Tools.GetRandomNumber(info.Settings.RedRangeMin, info.Settings.RedRangeMax);

                drawing.SetDirty();
            }

            if (Tools.WillMutate(info.Settings.ColorMutationRate))
            {
                Green = Tools.GetRandomNumber(info.Settings.GreenRangeMin, info.Settings.GreenRangeMax);

                drawing.SetDirty();
            }
            if (Tools.WillMutate(info.Settings.ColorMutationRate))
            {
                Blue = Tools.GetRandomNumber(info.Settings.BlueRangeMin, info.Settings.BlueRangeMax);

                drawing.SetDirty();
            }

            if (Tools.WillMutate(info.Settings.ColorMutationRate))
            {
                Alpha = Tools.GetRandomNumber(info.Settings.AlphaRangeMin, info.Settings.AlphaRangeMax);

                drawing.SetDirty();
            }
        }
    }
}
