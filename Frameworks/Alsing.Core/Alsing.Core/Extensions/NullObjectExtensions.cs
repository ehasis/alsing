// *
// * Copyright (C) 2008 Roger Alsing : http://www.rogeralsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

namespace Alsing.NullObjects
{
    public static class NullObjectExtensions
    {
        /// <summary>
        /// Registers input value as "null object" for type
        /// </summary>
        /// <typeparam name="T">Input value type</typeparam>
        /// <param name="item">Input value</param>
        public static void RegisterNullObject<T>(this T item) where T : class
        {
            NullObjectRepository<T>.NullObject = item;
        }

        /// <summary>
        /// Gets the current value if the input value is not null, else returning registered null object
        /// </summary>
        /// <typeparam name="T">Input value type</typeparam>
        /// <param name="item">Input value</param>
        /// <returns></returns>
        public static T CurrentOrNullObject<T>(this T item) where T : class
        {
            return NullObjectRepository<T>.NullObject;
        }

        #region Nested type: NullObjectRepository

        private static class NullObjectRepository<T>
        {
            public static T NullObject { get; set; }
        }

        #endregion
    }
}