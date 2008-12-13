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
        private double errorLevel = double.MaxValue;
        private bool didMutate = false;

        public DefaultEvolutionJob(Color[,] sourceColors)
        {
            SourceColors = sourceColors;
            currentDrawing = GetNewInitializedDrawing();
            errorLevel = FitnessCalculator.GetDrawingFitness(currentDrawing, SourceColors);
        }

        public bool DidMutate
        {
            get { return didMutate; }
        }

        private static DnaDrawing GetNewInitializedDrawing()
        {
            var drawing = new DnaDrawing();
            drawing.Init();
            return drawing;
        }

        #region IEvolutionJob Members

        public DnaDrawing CurrentDrawing
        {
            get { return currentDrawing; }
        }

        public DnaDrawing GetBestDrawing()
        {
            GetNextErrorLevel();
            return currentDrawing;
        }

        public double GetNextErrorLevel()
        {
            DnaDrawing newDrawing = currentDrawing.Clone();
            didMutate = false;

            newDrawing.Mutate();

            //TODO: Why not loop until we get a mutation - that way we don't waste lots of clones ^^
            if (newDrawing.IsDirty)
            {
                didMutate = true;

                double nextErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, SourceColors);

                if (nextErrorLevel <= errorLevel)
                {
                    errorLevel = nextErrorLevel;
                    currentDrawing = newDrawing;
                }

                return nextErrorLevel;
            }

            return errorLevel;
        }

        #endregion
    }
}
