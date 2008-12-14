using System.Drawing;
using GenArt.AST;
using GenArt.Core.Classes;

namespace GenArt.Classes
{
    public class LayeredWorker
    {
        public int MinIntensity { get; set; }
        public int MaxIntensity { get; set; }
        public DnaDrawing CurrentDrawing { get; set; }
        public double CurrentErrorLevel { get; set; }
        public bool IsDirty { get; set; }


        public LayeredWorker(SourceImage sourceImage)
        {
            CurrentDrawing = GetNewInitializedDrawing();
            CurrentDrawing.SourceImage = sourceImage;
            CurrentErrorLevel = double.MaxValue;
        }

        private static DnaDrawing GetNewInitializedDrawing()
        {
            var drawing = new DnaDrawing();
            drawing.Init();
            return drawing;
        }

        public DnaDrawing GetDrawing()
        {
            DnaDrawing newDrawing = CurrentDrawing.Clone();

            while (newDrawing.IsDirty == false)
                newDrawing.Mutate();

            double newErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, newDrawing.SourceImage);

            if (newErrorLevel <= CurrentErrorLevel)
            {
                CurrentDrawing = newDrawing;
                CurrentErrorLevel = newErrorLevel;
            }

            return CurrentDrawing;
        }
    }
}