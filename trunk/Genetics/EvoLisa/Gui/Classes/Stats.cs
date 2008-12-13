using System;
using System.Collections.Generic;
using System.Text;
using GenArt.AST;
using System.ComponentModel;

namespace GenArt.Core.Classes
{
    public class Stats
    {
        public Stats(DnaProject project)
        {
            this.project = project;
        }

        private DnaProject project;

        [Description("The fitness value shows how well the generated image matches the target image. The lower the value, the better the match.")]
        [Category("Evolution")]
        public double Fitness
        {
            get { return project.ErrorLevel; }
        }

        [Description("The number of generations that the evolution algorithm has been working on the DNA. Generations that do not include any mutations are included in this value.")]
        [Category("Evolution")]
        public int Generations
        {
            get { return project.Generations; }
        }

        [Description("The number of generations containing at least one mutation (which can be evaluated to see if it is better than the current champion).")]
        [Category("Evolution")]
        public int Mutations
        {
            get { return project.Mutations; }
        }

        [Description("The number of mutations that had an equally good or better (lower) fitness value than the previous champion (thus becoming the new champion).")]
        [Category("Evolution")]
        public int Selected
        {
            get { return project.Selected; }
        }

        [Description("The number of selected mutations that had a better (lower) fitness value than the previous champion.")]
        [Category("Evolution")]
        public int Positive
        {
            get { return project.Positive; }
        }

        [Description("The number of selected mutations that had an equally good fitness value as the previous champion.")]
        [Category("Evolution")]
        public int Neutral
        {
            get { return project.Neutral; }
        }

        [Description("The time that the evolution has been working.")]
        [Category("Evolution")]
        public TimeSpan ElapsedTime
        {
            get {
                return project.GetElapsedTime();
            }
        }

        [Description("The number of polygons on the canvas.")]
        [Category("Image")]
        public int Polygons
        {
            get { return project.Drawing.Polygons.Count; }
        }

        [Description("The number of points on the canvas.")]
        [Category("Image")]
        public int Points
        {
            get { return project.Drawing.PointCount; }
        }

        [Description("The average number of points per polygon.")]
        [Category("Averages")]
        public double PointsPerPolygon
        {
            get {
                int polys = project.Drawing.Polygons.Count;
                double avg = 0;
                if (polys > 0)
                    avg = ((double) project.Drawing.PointCount / polys);

                return avg; 
            }
        }

        [Description("The average number of generations per second.")]
        [Category("Averages")]
        public double GenerationsPerSecond
        {
            get
            {
                double avg = 0;
                TimeSpan elapsed = project.GetElapsedTime();
                if (elapsed > TimeSpan.MinValue)
                    avg = ((double)project.Generations / elapsed.Seconds);
                return avg;
            }
        }

        [Description("The average number of mutations per second.")]
        [Category("Averages")]
        public double MutationsPerSecond
        {
            get
            {
                double avg = 0;
                TimeSpan elapsed = project.GetElapsedTime();
                if (elapsed > TimeSpan.MinValue)
                    avg = ((double)project.Mutations / elapsed.Seconds);
                return avg;
            }
        }

        [Description("The average number of selected mutations per second.")]
        [Category("Averages")]
        public double SelectedPerSecond
        {
            get
            {
                double avg = 0;
                TimeSpan elapsed = project.GetElapsedTime();
                if (elapsed > TimeSpan.MinValue)
                    avg = ((double)project.Selected / elapsed.Seconds);
                return avg;
            }
        }

        [Description("The average number of selected mutations per second.")]
        [Category("Averages")]
        public double PositivePerSecond
        {
            get
            {
                double avg = 0;
                TimeSpan elapsed = project.GetElapsedTime();
                if (elapsed > TimeSpan.MinValue)
                    avg = ((double)project.Selected / elapsed.Seconds);
                return avg;
            }
        }

        [Description("The average number of mutations per generation.")]
        [Category("Averages")]
        public double MutationsPerGeneration
        {
            get 
            { 
                double avg = 0;
                if (project.Generations > 0)
                    avg = ((double) project.Mutations / project.Generations);
                return avg;
            }
        }

        [Description("The average number of selected mutations per generation.")]
        [Category("Averages")]
        public double SelectedPerGeneration
        {
            get
            {
                double avg = 0;
                if (project.Generations > 0)
                    avg = ((double) project.Selected / project.Generations);
                return avg;
            }
        }

        [Description("The average chance a mutation has of being selected.")]
        [Category("Averages")]
        public double SelectedPerMutation
        {
            get
            {
                double avg = 0;
                if (project.Mutations > 0)
                    avg = ((double) project.Selected / project.Mutations);
                return avg;
            }
        }

    }
}
