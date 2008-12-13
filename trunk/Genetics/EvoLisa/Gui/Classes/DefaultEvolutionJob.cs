using GenArt.AST;
using GenArt.Core.Classes;
using GenArt.Core.Interfaces;

namespace GenArt.Classes
{
    public class DefaultEvolutionJob : IEvolutionJob
    {
        private DnaDrawing currentDrawing;
        private double currentErrorLevel;

        public DefaultEvolutionJob(SourceImage sourceImage)
        {
            currentDrawing = GetNewInitializedDrawing();
            currentDrawing.SourceImage = sourceImage;
            currentErrorLevel = currentDrawing.GetErrorLevel();
        }

        #region IEvolutionJob Members

        public DnaDrawing GetBestDrawing()
        {
            GetNextErrorLevel();
            return currentDrawing;
        }

        #endregion

        private static DnaDrawing GetNewInitializedDrawing()
        {
            var drawing = new DnaDrawing();
            drawing.Init();
            return drawing;
        }

        public double GetNextErrorLevel()
        {
            DnaDrawing newDrawing = currentDrawing.Clone();

            newDrawing.Mutate();

            //TODO: Why not loop until we get a mutation - that way we don't waste lots of clones ^^
            if (newDrawing.IsDirty)
            {
                var newErrorLevel = newDrawing.GetErrorLevel();

                if (newErrorLevel <= currentErrorLevel)
                {
                    currentDrawing = newDrawing;
                    currentErrorLevel = newErrorLevel;
                }

                return newErrorLevel;
            }

            return currentErrorLevel;
        }
    }
}