using System;
using System.Collections.Generic;
using System.Text;
using GenArt.Classes;

namespace GenArt
{
    public static class IntExtensions
    {
        public static int Max(this int self, int max)
        {
            return Math.Max(self, max);
        }

        public static int Min(this int self, int min)
        {
            return Math.Min(self, min);
        }

        public static int Randomize(this int self, int min, int max)
        {
            return self + Tools.GetRandomNumber(min, max);
        }
    }
}

namespace System.Runtime.CompilerServices
{
    public class ExtensionAttribute : Attribute { }
}
