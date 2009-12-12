using System;

namespace Alsing
{
    public static class Curry
    {
        //---- 

        public static TResult Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6,
            T7 arg7)
        {
            return func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static Func<T7, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            return arg7 => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static Func<T6, T7, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return (arg6, arg7) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static Func<T5, T6, T7, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return (arg5, arg6, arg7) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static Func<T4, T5, T6, T7, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 arg1, T2 arg2, T3 arg3)
        {
            return (arg4, arg5, arg6, arg7) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static Func<T3, T4, T5, T6, T7, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 arg1, T2 arg2)
        {
            return (arg3, arg4, arg5, arg6, arg7) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static Func<T2, T3, T4, T5, T6, T7, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 arg1)
        {
            return (arg2, arg3, arg4, arg5, arg6, arg7) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        //---- 

        public static TResult Apply<T1, T2, T3, T4, T5, T6, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            return func(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static Func<T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return arg6 => func(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static Func<T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return (arg5, arg6) => func(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static Func<T4, T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 arg1, T2 arg2, T3 arg3)
        {
            return (arg4, arg5, arg6) => func(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static Func<T3, T4, T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 arg1, T2 arg2)
        {
            return (arg3, arg4, arg5, arg6) => func(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static Func<T2, T3, T4, T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(
            this Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 arg1)
        {
            return (arg2, arg3, arg4, arg5, arg6) => func(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        //---- 

        public static TResult Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func,
                                                                       T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return func(arg1, arg2, arg3, arg4, arg5);
        }

        public static Func<T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func,
                                                                           T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return arg5 => func(arg1, arg2, arg3, arg4, arg5);
        }

        public static Func<T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(
            this Func<T1, T2, T3, T4, T5, TResult> func, T1 arg1, T2 arg2, T3 arg3)
        {
            return (arg4, arg5) => func(arg1, arg2, arg3, arg4, arg5);
        }

        public static Func<T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(
            this Func<T1, T2, T3, T4, T5, TResult> func, T1 arg1, T2 arg2)
        {
            return (arg3, arg4, arg5) => func(arg1, arg2, arg3, arg4, arg5);
        }

        public static Func<T2, T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(
            this Func<T1, T2, T3, T4, T5, TResult> func, T1 arg1)
        {
            return (arg2, arg3, arg4, arg5) => func(arg1, arg2, arg3, arg4, arg5);
        }

        //---- 

        public static TResult Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 arg1,
                                                                   T2 arg2, T3 arg3, T4 arg4)
        {
            return func(arg1, arg2, arg3, arg4);
        }

        public static Func<T4, TResult> Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 arg1,
                                                                       T2 arg2, T3 arg3)
        {
            return arg4 => func(arg1, arg2, arg3, arg4);
        }

        public static Func<T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func,
                                                                           T1 arg1, T2 arg2)
        {
            return (arg3, arg4) => func(arg1, arg2, arg3, arg4);
        }

        public static Func<T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func,
                                                                               T1 arg1)
        {
            return (arg2, arg3, arg4) => func(arg1, arg2, arg3, arg4);
        }

        //---- 

        public static TResult Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2,
                                                               T3 arg3)
        {
            return func(arg1, arg2, arg3);
        }

        public static Func<T3, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 arg1, T2 arg2)
        {
            return arg3 => func(arg1, arg2, arg3);
        }

        public static Func<T2, T3, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 arg1)
        {
            return (arg2, arg3) => func(arg1, arg2, arg3);
        }

        //---- 

        public static TResult Apply<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
        {
            return func(arg1, arg2);
        }

        public static Func<T2, TResult> Apply<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 arg1)
        {
            return arg2 => func(arg1, arg2);
        }

        //---- 


    }
}