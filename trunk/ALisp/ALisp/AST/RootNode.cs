using Alsing.Lisp.Infrastructure;
using Alsing.Lisp.Runtime;

namespace Alsing.Lisp.AST
{
    public class RootNode : ConsNode
    {
        public override object Eval(StackFrame stackFrame)
        {
            object res = null;
            foreach (object arg in Args)
            {
                res = Utils.Eval(stackFrame, arg);
            }
            return res;
        }

        public override object Clone(CloneInfo info)
        {
            return null;
        }
    }
}