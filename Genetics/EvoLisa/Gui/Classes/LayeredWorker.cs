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
        private Settings settings;


        public LayeredWorker(SourceImage sourceImage, Settings settings)
        {
            this.settings = settings;
            CurrentDrawing = GetNewInitializedDrawing(settings);
            CurrentDrawing.SourceImage = sourceImage;
            CurrentErrorLevel = double.MaxValue;
        }

        private static DnaDrawing GetNewInitializedDrawing(Settings settings)
        {
            var drawing = new DnaDrawing();
            drawing.Init(settings);
            return drawing;
        }

        public DnaDrawing GetDrawing()
        {
            DnaDrawing newDrawing = CurrentDrawing.Clone();

            newDrawing.Mutate(settings);

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