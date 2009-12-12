// *
// * Copyright (C) 2008 Roger Alsing : http://www.rogeralsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System.Collections.Generic;
using System.Linq;
using System.Threading;

/*
                SAMPLE USAGE
 
                //declare the variables we want to assign
                string str = null;
                int val = 0;

                //start a new async fork
                //assign the variables inside the fork 

                Fork.Begin()
                    .Call(() => str = CallSomeWebService (123,"abc") )
                    .Call(() => val = ExecSomeStoredProc ("hello") )
                    .End(); 

                //the fork has finished 

                //we can use the variables now
                Console.WriteLine("{0} {1}", str, val);
 
 */

namespace Alsing.Threading
{
    public delegate void ForkCall();

    public class Fork
    {
        private readonly List<ForkCall> calls = new List<ForkCall>();

        /// <summary>
        /// Starts an async fork
        /// </summary>
        /// <returns>Returns a new fork instance</returns>
        public static Fork Begin()
        {
            return new Fork();
        }

        /// <summary>
        /// Queues a call in the fork
        /// </summary>
        /// <param name="call">Delegate that should be executed async</param>
        /// <returns>Returns self</returns>
        public Fork Call(ForkCall call)
        {
            calls.Add(call);
            return this;
        }

        /// <summary>
        /// Executes all the calls async and waits untill all of them are finished
        /// </summary>
        public void End()
        {
            //convert all calls to running threads
            //then wait for all threads to finish
            calls.Select(call => GetThread(call))
                .ToList()
                .ForEach(thread => thread.Join());
        }

        private static Thread GetThread(ForkCall call)
        {
            var thread = new Thread(GetThreadStart(call)) {IsBackground = true};
            thread.Start();
            return thread;
        }

        private static ThreadStart GetThreadStart(ForkCall call)
        {
            ThreadStart ts = () => call();
            return ts;
        }
    }
}