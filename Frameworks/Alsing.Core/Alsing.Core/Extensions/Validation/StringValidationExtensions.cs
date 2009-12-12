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
using System.Diagnostics;

namespace Alsing
{
    public static class StringValidationExtensions
    {
        [DebuggerHidden]
        public static Validation<string> ShorterThan(this Validation<string> item, int limit)
        {
            if (item.Value.Length >= limit)
                throw new ArgumentException( string.Format("Parameter {0} must be shorter than {1} chars",item.ArgName,
                                                                                                      limit));

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> LongerThan(this Validation<string> item, int limit)
        {
            if (item.Value.Length <= limit)
                throw new ArgumentException( string.Format("Parameter {0} must be longer than {1} chars",item.ArgName, limit));

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> StartsWith(this Validation<string> item, string pattern)
        {
            if (!item.Value.StartsWith(pattern))
                throw new ArgumentException(string.Format("Parameter {0} must start with {1}",item.ArgName, pattern));

            return item;
        }

        //Contributed by Andreas Håkansson
        [DebuggerHidden]
        public static Validation<string> ExactLenght(this Validation<string> item, int length)
        {
            if (item.Value.Length != length)
                throw new ArgumentOutOfRangeException(item.ArgName, item.Value,
                                                     string.Format( "Parameter {0} has to be {1} characters long.",
                                                          item.ArgName, length));
            return item;
        }

        [DebuggerHidden]
        public static Validation<string> NotEmpty(this Validation<string> item)
        {
            if (item == "")
                throw new ArgumentException(string.Format("Parameter {0} may not be empty",item.ArgName), item.ArgName);

            return item;
        }

        [DebuggerHidden]
        public static Validation<string> NotNullOrEmpty(this Validation<string> item)
        {
            item.NotNull();
            item.NotEmpty();

            return item;
        }
    }
}