using System;
using System.Collections.Generic;
using System.Text;
using GenArt.AST;
using GenArt.Core.Classes;
using GenArt.Core.Interfaces;
using System.Drawing;

namespace GenArt.Classes
{
    public class DefaultEvolutionJob : IEvolutionJob
    {
        private DnaDrawing currentDrawing;


        public DefaultEvolutionJob(SourceImage sourceImage)
        {
            currentDrawing = GetNewInitializedDrawing();
            currentDrawing.SourceImage = sourceImage;
            currentDrawing.ErrorLevel = FitnessCalculator.GetDrawingFitness(currentDrawing, sourceImage);
        }


        private static DnaDrawing GetNewInitializedDrawing()
        {
            var drawing = new DnaDrawing();
            drawing.Init();
            return drawing;
        }

        #region IEvolutionJob Members

        public DnaDrawing GetBestDrawing()
        {
            GetNextErrorLevel();
            return currentDrawing;
        }

        public double GetNextErrorLevel()
        {
            DnaDrawing newDrawing = currentDrawing.Clone();

            newDrawing.Mutate();

            //TODO: Why not loop until we get a mutation - that way we don't waste lots of clones ^^
            if (newDrawing.IsDirty)
            {

                newDrawing.ErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, newDrawing.SourceImage);

                if (newDrawing.ErrorLevel <= currentDrawing.ErrorLevel)
                {
                    currentDrawing = newDrawing;
                }

                return newDrawing.ErrorLevel;
            }

            return currentDrawing.ErrorLevel;
        }

        #endregion
    }
}
