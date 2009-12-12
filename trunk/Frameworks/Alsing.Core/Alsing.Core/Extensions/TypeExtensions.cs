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
    public static class TypeExtensions
    {
        //Experimental, ignore
        public static T Transform<T>(this T item, Func<T, T> selector)
        {
            return selector(item);
        }

        /// <summary>
        /// Fluent version of C# "as" keyword
        /// </summary>
        /// <typeparam name="T">Type to cast to</typeparam>
        /// <param name="item">value to be casted</param>
        /// <returns>casted value</returns>
        public static T As<T>(this object item)
        {
            if (item is T)
                return (T) item;

            return default(T);
        }

        /// <summary>
        /// Fluent version of type casts
        /// </summary>
        /// <typeparam name="T">Type to cast to</typeparam>
        /// <param name="item">value to be casted</param>
        /// <returns>casted value</returns>
        public static T Cast<T>(this object item)
        {
            if (item is T)
                return (T) item;

            throw new InvalidCastException(string.Format("Can not cast from type {0} to {1}",item.GetType().Name, typeof (T).Name));
        }

        /// <summary>
        /// Creates an instance of the given type
        /// </summary>
        /// <param name="type">Type to instantiate</param>
        /// <returns>Instance of the given Type</returns>
        public static object New(this Type type)
        {
            type.Require("type")
                .NotNull();

            return Activator.CreateInstance(type);
        }
    }
}