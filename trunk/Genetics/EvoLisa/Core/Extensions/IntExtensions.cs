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

        public static int Randomize(this int self,JobInfo info, int min, int max)
        {
            return self + info.GetRandomNumber(min, max);
        }
    }
}

//hack to use extensions in .net 2.0
namespace System.Runtime.CompilerServices
{
    public class ExtensionAttribute : Attribute { }
}
