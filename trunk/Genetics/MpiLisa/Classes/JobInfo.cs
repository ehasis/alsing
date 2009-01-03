using System;
using GenArt.Core.Classes;

namespace GenArt.Classes
{
    internal class JobInfo
    {
        private Random random;
        internal SourceImage SourceImage { get; set; }
        internal Settings Settings { get; set; }

        internal void InitRandom(int seed)
        {
            random = new Random(seed);
        }


        internal int GetRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        internal bool WillMutate(int mutationRate)
        {
            int val = GetRandomNumber(0, mutationRate);
            return val == 1;
        }
    }
}