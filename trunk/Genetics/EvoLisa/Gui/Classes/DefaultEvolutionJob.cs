using System;
using System.Collections.Generic;
using System.Text;
using GenArt.AST;
using GenArt.Core.Interfaces;
using System.Drawing;

namespace GenArt.Classes
{
    public class DefaultEvolutionJob : IEvolutionJob
    {
        private DnaDrawing currentDrawing;
        public Color[,] SourceColors { get; set; }

        public DefaultEvolutionJob(Color[,] sourceColors)
        {
            SourceColors = sourceColors;
            currentDrawing = GetNewInitializedDrawing();
            currentDrawing.ErrorLevel = FitnessCalculator.GetDrawingFitness(currentDrawing, SourceColors);
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
            DnaDrawing newDrawing;
            lock (currentDrawing)
            {
                newDrawing = currentDrawing.Clone();
            }
            newDrawing.Mutate();

            if (newDrawing.IsDirty)
            {

                newDrawing.ErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, SourceColors);

                if (newDrawing.ErrorLevel <= currentDrawing.ErrorLevel)
                {
                    lock (currentDrawing)
                    {
                        currentDrawing = newDrawing;
                    }
                }
            }

            return currentDrawing;
        }

        #endregion
    }
}
