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

        public void Init(Settings settings)
        {
            Red = Tools.GetRandomNumber(settings.RedRangeMin, settings.RedRangeMax);
            Green = Tools.GetRandomNumber(settings.GreenRangeMin, settings.GreenRangeMax);
            Blue = Tools.GetRandomNumber(settings.BlueRangeMin, settings.BlueRangeMax);
            Alpha = Tools.GetRandomNumber(settings.AlphaRangeMin, settings.AlphaRangeMax);
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

        public void Mutate(DnaDrawing drawing, Settings settings)
        {
            if (Tools.WillMutate(settings.ColorMutationRate) )
            {
                Red = Red
                    .Randomize(-15, 15)
                    .Min(255)
                    .Max(0);

                Green = Green
                    .Randomize(-15, 15)
                    .Min(255)
                    .Max(0);

                Blue = Blue
                    .Randomize(-15, 15)
                    .Min(255)
                    .Max(0);

                Alpha = Alpha
                    .Randomize(-5, 5)
                    .Min(settings.AlphaRangeMax)
                    .Max(settings.AlphaRangeMin);

                drawing.SetDirty();
            }
        }
    }
}