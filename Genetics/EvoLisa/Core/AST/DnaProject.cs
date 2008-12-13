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
            Drawing = new DnaDrawing();
            Drawing.Init();
            Settings = new Settings();
            ErrorLevel = double.MaxValue;
            LastSavedFitness = double.MaxValue;
        }

        [XmlIgnore]
        public bool IsRunning { get; set; }

        public string ImagePath { get; set; }

        public DnaDrawing Drawing { get; set; }
        public Settings Settings { get; set; }

        public double ErrorLevel { get; set; }
        public int Generations { get; set; }
        public int Mutations { get; set; }
        public int Selected { get; set; }
        public int Positive { get; set; }
        public int Neutral { get; set; }

        public double LastSavedFitness { get; set; }
        public int LastSavedSelected { get; set; }

        public DateTime LastStartTime { get; set; }
        public TimeSpan ElapsedTime { get; set; }

        internal TimeSpan GetElapsedTime()
        {
            TimeSpan elapsed = ElapsedTime;
            if (IsRunning)
                elapsed += DateTime.Now - LastStartTime;
            return elapsed;
        }
    }
}
