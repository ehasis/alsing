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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Alsing
{
    public static class ValidatorExtensions
    {
        public static Validation<T> Require<T>(this T item, string argName)
        {
            return new Validation<T>(item, argName);
        }

        public static Validation<T> Require<T>(this T item)
        {
            return new Validation<T>(item, "value");
        }
    }


    public class Validation<T>
    {
        public Validation(T value, string argName)
        {
            Value = value;
            ArgName = argName;
        }

        public T Value { get; set; }
        public string ArgName { get; set; }

        public static implicit operator T(Validation<T> item)
        {
            return item.Value;
        }
    }



    public static class ValidationExtender
    {        
        [DebuggerHidden]               
        public static Validation<T> NotNull<T>(this Validation<T> item) where T : class
        {
            if (item.Value == null)
                throw new ArgumentNullException(item.ArgName);

            return item;
        }

        //Contributed by Andreas Håkansson
        [DebuggerHidden]
        public static Validation<T> ExistsInList<T>(this Validation<T> item, IList<T> list)
        {
            if (!list.Contains(item.Value))
                throw new ArgumentException(string.Format("The value {0} did not exist in the provided list of valid values.",item.ArgName), item.ArgName);

            return item;
        }

        //Contributed by Andreas Håkansson
        [DebuggerHidden]
        public static Validation<T> IsInRange<T>(this Validation<T> item, T lowerBoundry, T upperBoundry)
            where T : IComparable
        {

            if ((item.Value.CompareTo(lowerBoundry) < 0) || (item.Value.CompareTo(upperBoundry) > 0))
                throw new ArgumentOutOfRangeException( item.ArgName, item.Value,
                                                      string.Format("Parameter {0} cannot be less than {1} or greater than {2}",item.ArgName, lowerBoundry, upperBoundry));

            return item;
        }

        public static Validation<T> IsGreaterThan<T>(this Validation<T> item,T other) where T : IComparable
        {
            if ((item.Value.CompareTo(other) <= 0))
                throw new ArgumentOutOfRangeException(item.ArgName, item.Value,string.Format("Parameter {0} must be greater than {1} ",item.ArgName, other));
            
            return item;
        }

        public static Validation<T> IsLessThan<T>(this Validation<T> item, T other) where T : IComparable
        {
            if ((item.Value.CompareTo(other) >= 0))
                throw new ArgumentOutOfRangeException(item.ArgName, item.Value,string.Format("Parameter {0} must be less than {1} ",item.ArgName, other));

            return item;
        }

        public static Validation<T> IsEqualTo<T>(this Validation<T> item, T other) where T : IComparable
        {
            if ((item.Value.CompareTo(other) != 0))
                throw new ArgumentOutOfRangeException(item.ArgName, item.Value,string.Format("Parameter {0} must be Equal to {1} ",item.ArgName, other));

            return item;
        }

        [DebuggerHidden]
        public static TypeCheck<T> TypeCheck<T>(this Validation<T> item)
        {
            var typeCheck = new TypeCheck<T>(item);
            return typeCheck;
        }

        [DebuggerHidden]
        public static Validation<T> Eval<T>(this Validation<T> item, Expression<Func<T,bool>> expression)
        {
            expression.Require("expression")
                .NotNull();

            var del = expression.Compile();

            bool res = del(item.Value);

            if (!res)
            {
                var lambda = expression as LambdaExpression;
                string expressionText = lambda.Body.ToString();
                throw new ArgumentException(string.Format("Expression '{0}' evaluated false", expressionText), item.ArgName);
            }

            return item;
        }

        [DebuggerHidden]
        public static Validation<T> Eval<T>(this Validation<T> item, bool expression)
        {            
            if (!expression)
                throw new ArgumentException("Expression evaluated false", item.ArgName);

            return item;
        }
    }
}