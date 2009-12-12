// *
// * Copyright (C) 2008 Roger Alsing : http://www.rogeralsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using System.Diagnostics;
using Alsing.Logging;

namespace Alsing
{
/*
                SAMPLE USAGE
  
                "Hello World".Output();
                myAge.FormatAs("My age is: {0}").Output();
   
 
                Customize output with:
                
                SimpleLog.Logger.Out = SomeTextWriter;
                SimpleLog.Logger.Formatter = s => s.FormatAs("Formatted output ** {0} **");
 
                Applies to: Logger/Debugger/Output
*/

    public static class LogExtensions
    {
        /// <summary>
        /// Outputs the input value to std output
        /// </summary>
        /// <param name="item">Input value</param>
        public static void Output(this object item)
        {
            SimpleLog.Output.WriteLine(item);
        }

        /// <summary>
        /// Outputs the input value to the debugger
        /// </summary>
        /// <param name="item">Input value</param>
        [Conditional("DEBUG")]
        public static void Debug(this object item)
        {
            SimpleLog.Debugger.WriteLine(item);
        }

        /// <summary>
        /// Outputs the input value to custom user logger.
        /// (Defaults to std output)
        /// </summary>
        /// <param name="item">Input value</param>
        public static void Log(this object item)
        {
            SimpleLog.Logger.WriteLine(item);
        }
    }
}