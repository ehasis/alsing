using System;
using System.Collections.Generic;
using System.Text;
using GenArt.Classes;

namespace GenArt
{
    internal static class IntExtensions
    {
        internal static int Max(this int self, int max)
        {
            return Math.Max(self, max);
        }

        internal static int Min(this int self, int min)
        {
            return Math.Min(self, min);
        }

        internal static int Randomize(this int self,JobInfo info, int min, int max)
        {
            return self + info.GetRandomNumber(min, max);
        }
    }
}
