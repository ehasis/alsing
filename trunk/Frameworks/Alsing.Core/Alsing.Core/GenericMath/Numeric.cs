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
using System.Linq.Expressions;

namespace Alsing
{
    /*
                        SAMPLE USAGE
                
                        public T GenericFunc<T> (T a,T b)
                        {
                            Numeric<T> numA = a;
                            Numeric<T> numB = b;
     
                            return numA + numB;
                        }      
     */

    public class Numeric<T>
    {
        private static readonly Func<T, T, T> Add = CompileDelegate(Expression.Add);
        private static readonly Func<T, T, T> Div = CompileDelegate(Expression.Divide);
        private static readonly Func<T, T, T> Mod = CompileDelegate(Expression.Modulo);
        private static readonly Func<T, T, T> Mul = CompileDelegate(Expression.Multiply);
        private static readonly Func<T, T, T> Sub = CompileDelegate(Expression.Subtract);

        private static readonly Func<T, T, bool> Equal = CompileCompareDelegate(Expression.Equal);
        private static readonly Func<T, T, bool> NotEqual = CompileCompareDelegate(Expression.NotEqual);
        private static readonly Func<T, T, bool> GreaterThan = CompileCompareDelegate(Expression.GreaterThan);
        private static readonly Func<T, T, bool> GreaterThanOrEqual = CompileCompareDelegate(Expression.GreaterThanOrEqual);
        private static readonly Func<T, T, bool> LessThan = CompileCompareDelegate(Expression.LessThan);
        private static readonly Func<T, T, bool> LessThanOrEqual = CompileCompareDelegate(Expression.LessThanOrEqual);

        
        

        public static readonly T One = FromInt(1);
        public static readonly T Zero = FromInt(1);

        private static T FromInt(int value)
        {
            return (T)Convert.ChangeType(value,typeof(T));
        }
        
        private static bool isNumeric = true;

        private readonly T value;

        public Numeric(T value)
        {
            this.value = value;            
        }

        public T Value
        {
            get { return value; }
        }

        public static bool IsNumeric
        {
            get
            {
                return isNumeric;
            }
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equal(this.value, obj.As<Numeric<T>>().value);
        }



        private static Func<T, T, T> CompileDelegate(Func<Expression, Expression, Expression> operation)
        {
            try
            {
                //create two inprameters
                ParameterExpression leftExp = Expression.Parameter(typeof (T), "left");
                ParameterExpression rightExp = Expression.Parameter(typeof (T), "right");

                //create the body from the delegate that we passed in
                Expression body = operation(leftExp, rightExp);

                //create a lambda that takes two args of T and returns T
                LambdaExpression lambda = Expression.Lambda(typeof (Func<T, T, T>), body, leftExp, rightExp);

                //compile the lambda to a delegate that takes two args of T and returns T
                var compiled = (Func<T, T, T>) lambda.Compile();
                return compiled;
            }
            catch
            {
                isNumeric = false;

                //type T does not support math operations
                
                //Giving credit where credit is due:
                //This part is a ripp of from Marc Gravell generic math implementation               
                return (a, b) => { throw new NotSupportedException("Type '{0}' does not support math operations".FormatWith(typeof(T).Name)); };
            }        
        }

        private static Func<T, T, bool> CompileCompareDelegate(Func<Expression, Expression, Expression> operation)
        {
            try
            {
                //create two inprameters
                ParameterExpression leftExp = Expression.Parameter(typeof(T), "left");
                ParameterExpression rightExp = Expression.Parameter(typeof(T), "right");

                //create the body from the delegate that we passed in
                Expression body = operation(leftExp, rightExp);

                //create a lambda that takes two args of T and returns T
                LambdaExpression lambda = Expression.Lambda(typeof(Func<T, T, bool>), body, leftExp, rightExp);

                //compile the lambda to a delegate that takes two args of T and returns T
                var compiled = (Func<T, T, bool>)lambda.Compile();
                return compiled;
            }
            catch
            {
                //type T does not support math operations

                //Giving credit where credit is due:
                //This part is a ripp of from Marc Gravell generic math implementation               
                return (a, b) => { throw new NotSupportedException("Type '{0}' does not support math operations".FormatWith(typeof(T).Name)); };
            }
        }

        public static implicit operator Numeric<T>(T a)
        {
            return new Numeric<T>(a);
        }

        public static implicit operator T(Numeric<T> Numeric)
        {
            return Numeric.value;
        }

        public static Numeric<T> operator +(Numeric<T> a, Numeric<T> b)
        {
            return Add(a, b);
        }

        public static Numeric<T> operator -(Numeric<T> a, Numeric<T> b)
        {
            return Sub(a, b);
        }

        public static Numeric<T> operator /(Numeric<T> a, Numeric<T> b)
        {
            return Div(a, b);
        }

        public static Numeric<T> operator *(Numeric<T> a, Numeric<T> b)
        {
            return Mul(a, b);
        }

        public static Numeric<T> operator %(Numeric<T> a, Numeric<T> b)
        {
            return Mod(a, b);
        }

        public static Numeric<T> operator ++(Numeric<T> a)
        {
            return a + One;
        }

        public static Numeric<T> operator --(Numeric<T> a)
        {
            return a - One;
        }

        public static bool operator ==(Numeric<T> a,Numeric<T> b)
        {
            return Equal(a, b);
        }

        public static bool operator !=(Numeric<T> a, Numeric<T> b)
        {
            return NotEqual(a, b);
        }

        public static bool operator >(Numeric<T> a, Numeric<T> b)
        {
            return GreaterThan(a, b);
        }

        public static bool operator <(Numeric<T> a, Numeric<T> b)
        {
            return LessThan(a, b);
        }

        public static bool operator >=(Numeric<T> a, Numeric<T> b)
        {
            return GreaterThanOrEqual(a, b);
        }

        public static bool operator <=(Numeric<T> a, Numeric<T> b)
        {
            return LessThanOrEqual(a, b);
        }


        public override string ToString()
        {
            return Value.FormatAs("Numeric: {0}");
        }
    }
}