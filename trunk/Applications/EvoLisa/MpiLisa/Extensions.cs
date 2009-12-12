using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpiLisa
{
    internal static class Extensions
    {
        internal static IEnumerable<int> Repeat(this int max)
        {
            for (int i=0;i<max;i++)
            {
                yield return i;
            }
        }
    }
}
