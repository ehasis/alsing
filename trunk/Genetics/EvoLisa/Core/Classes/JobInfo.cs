using System;
using System.Collections.Generic;
using System.Text;
using GenArt.Core.Classes;

namespace GenArt.Classes
{
    public class JobInfo
    {
        public SourceImage SourceImage { get; set; }
        public Settings Settings { get; set; }
        private Random random;

        public void InitRandom(int seed)
        {
            random = new Random(seed);
        }


        public int GetRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public bool WillMutate(int mutationRate)
        {
            int val = GetRandomNumber(0, mutationRate);
            return val == 1;
        }
    }
}
