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
using System.IO;

namespace Alsing.Logging
{
    public class SimpleLog
    {
        public static SimpleLog Debugger = new SimpleLog();
        public static SimpleLog Logger = new SimpleLog();
        public static SimpleLog Output = new SimpleLog();

        public SimpleLog()
        {
            Out = Console.Out;
            Formatter = s => s;
        }

        public TextWriter Out { get; set; }
        public Func<string, string> Formatter { get; set; }

        public void WriteLine(object item)
        {
            string input = string.Format("{0}", item);
            string output = Formatter(input);
            Out.WriteLine(output);
        }
    }
}