// *
// * Copyright (C) 2008 Roger Alsing : http://www.rogeralsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System;

namespace Alsing
{
    public static class NumericExtensions
    {
        public static void Times(this int times, Action action)
        {
            for (int i = 0; i < times; i++)
            {
                action();
            }
        }

        public static void Times(this int times, Action<int> action)
        {
            for (int i = 0; i < times; i++)
            {
                action(i);
            }
        }


        public static Numeric<double> AsNumeric(this double value)
        {
            return new Numeric<double>(value);
        }

        public static Numeric<float> AsNumeric(this float value)
        {
            return new Numeric<float>(value);
        }

        public static Numeric<int> AsNumeric(this int value)
        {
            return new Numeric<int>(value);
        }

        public static Numeric<long> AsNumeric(this long value)
        {
            return new Numeric<long>(value);
        }

        public static Numeric<decimal> AsNumeric(this decimal value)
        {
            return new Numeric<decimal>(value);
        }
    }
}