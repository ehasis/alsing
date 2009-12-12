using System.Collections.Generic;

namespace Alsing.Lisp.Infrastructure
{
    public class CloneInfo
    {
        public StackFrame StackFrame { get; set; }
        public List<string> LocalIdentifiers { get; set; }
        public bool BackQuote { get; set; }
    }
}