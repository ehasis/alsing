using System;
using System.Collections.Generic;
using System.Text;
using GenArt.Classes;
using System.Xml.Serialization;

namespace GenArt.AST
{
    public class DnaProject
    {
        public void Init()
        {
            Settings = new Settings();
            Drawing = new DnaDrawing();
          //  Drawing.Init(Settings);
            ErrorLevel = double.MaxValue;
            LastSavedFitness = double.MaxValue;
        }

        [XmlIgnore]
        private bool isRunning = false;
        public bool IsRunning 
        {
            get { return isRunning; }
            set { isRunning = value; } 
        }

        private string imagePath ;

        private DnaDrawing drawing ;
        private Settings settings ;

        private double errorLevel ;
        private int generations ;
        private int mutations ;
        private int selected ;
        private int positive ;
        private int neutral ;

        private double lastSavedFitness ;
        private int lastSavedSelected ;

        private DateTime lastStartTime ;
        private TimeSpan elapsedTime ;


        public string ImagePath 
        {
            get { return imagePath; }
            set { imagePath = value; } 
        }

        public DnaDrawing Drawing 
        {
            get { return drawing; }
            set { drawing = value; } 
        }
        public Settings Settings 
        {
            get { return settings; }
            set { settings = value; } 
        }

        public double ErrorLevel 
        {
            get { return errorLevel; }
            set { errorLevel = value; } 
        }
        public int Generations 
        {
            get { return generations; }
            set { generations = value; } 
        }
        public int Mutations 
        {
            get { return mutations; }
            set { mutations = value; } 
        }
        public int Selected 
        {
            get { return selected; }
            set { selected = value; } 
        }
        public int Positive 
        {
            get { return positive; }
            set { positive = value; } 
        }
        public int Neutral 
        {
            get { return neutral; }
            set { neutral = value; } 
        }

        public double LastSavedFitness 
        {
            get { return lastSavedFitness; }
            set { lastSavedFitness = value; } 
        }
        public int LastSavedSelected 
        {
            get { return lastSavedSelected; }
            set { lastSavedSelected = value; } 
        }

        public DateTime LastStartTime 
        {
            get { return lastStartTime; }
            set { lastStartTime = value; } 
        }
        public TimeSpan ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; }
        }

        public TimeSpan GetElapsedTime()
        {
            TimeSpan elapsed = ElapsedTime;
            if (IsRunning)
                elapsed += DateTime.Now - LastStartTime;
            return elapsed;
        }

    }
}
