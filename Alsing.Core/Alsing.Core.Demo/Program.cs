using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Alsing;
using Alsing.Threading;

namespace ExtensionDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {            
            Func<double, double, double> pow = Math.Pow;
            var res = pow.Apply(3,2);

            res
                .FormatAs("res {0}")
                .Output();
            
            


            //List<string> list = new List<string>()
            //                    {
            //                        "a",
            //                        "b",
            //                        "c",
            //                        "d",
            //                        "e"
            //                    };

            //foreach(var entry in list.GetEntries())
            //{
            //    "#{0} = {1} : First {2} , Last {3}"
            //        .FormatWith(entry.Index, entry.Item, entry.IsFirst, entry.IsLast)                    
            //        .Output();
            //}
            

            ////demo of fluent argument validation specification
            //string valRes = ValidationFunc(20, "Roger", new DateTime(2005, 01, 01));

            ////demo of normal calls
            //CallSync();
            ////demo of fork
            //CallAsync();

            Console.ReadLine();
        }

        private static int ForkIt()
        {
            int a = 0;
            int b = 0;

            Fork.Begin()
                .Call(() => a = SomeSlowCall())
                .Call(() => b = SomeOtherSlowCall())
                .End();

            return a + b;
        }

        private static string ValidationFunc(int a, string b, DateTime c)
        {
            //pre conditions:

            a.Require("a")
                .IsGreaterThan(10);

            b.Require("b")
                .NotNull()
                .NotEmpty()
                .LongerThan(2)
                .StartsWith("Ro");

            c.Require("c")
                .IsInRange(new DateTime(2000, 01, 01),
                           new DateTime(2010, 01, 01));

            //Do stuff
            //--- lots of code --- :-)

            const string res = "Foo";

            //post condition:

            return res.Require("res")
                .NotNull()
                .LongerThan(2)
                .ShorterThan(100);
        }


        //takes 2.0 sec to execute
        private static int SomeOtherSlowCall()
        {
            Thread.Sleep(2000);
            return 543;
        }

        //takes 2.0 sec to execute
        private static int SomeSlowCall()
        {
            Thread.Sleep(2000);
            return 123;
        }

        private static void CallSync()
        {
            var sw = new Stopwatch();
            "Calling Sync".Output();
            sw.Start();
            SomeSlowCall();
            SomeOtherSlowCall();
            sw.Stop();
            "Sync completed".Output();
            sw.Elapsed
                .FormatAs("Elapsed time: {0}")
                .Output();
        }

        //see ForkIt for async fork usage
        private static void CallAsync()
        {
            var sw = new Stopwatch();
            "Calling Fork".Output();
            sw.Start();
            int forkRes = ForkIt();
            sw.Stop();
            "Fork completed".Output();
            sw.Elapsed
                .FormatAs("Elapsed time: {0}")
                .Output();
        }
    }
}