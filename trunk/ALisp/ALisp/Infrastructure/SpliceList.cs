using System;
using System.Collections;

namespace Alsing.Lisp.Infrastructure
{
    public class Splice
    {
        public Splice(IEnumerable list)
        {
            if (list == null)
                throw new Exception("List may not be null");
            List = list;
        }

        public IEnumerable List { get; set; }
    }
}