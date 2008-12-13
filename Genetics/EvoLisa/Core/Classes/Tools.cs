using System;

namespace GenArt.Classes
{
    public static class Tools
    {
        private static readonly Random random = new Random();


        public static int GetRandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        public static bool WillMutate(int mutationRate)
        {
            if (GetRandomNumber(0, mutationRate) == 1)
                return true;
            return false;
        }
    }
}