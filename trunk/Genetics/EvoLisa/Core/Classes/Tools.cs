using System;

namespace GenArt.Classes
{
    public static class Tools
    {
        [ThreadStatic]
        private static Random random;

        public static void InitRandom(int seed)
        {
            random = new Random(seed);
        }


        public static int GetRandomNumber(int min, int max)
        {
            //thread static hack
            if (random == null)
                random = new Random();

            return random.Next(min, max);
        }

        public static bool WillMutate(int mutationRate)
        {
            int val = GetRandomNumber(0, mutationRate);
            return val == 1;
        }
    }
}