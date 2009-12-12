using System.Collections.Generic;
using Alsing.Lisp.Runtime;

namespace Alsing.Lisp.Infrastructure
{
    public struct FunctionInvocation
    {
        public List<object> Args;
        public LispFunc Func;
        public StackFrame StackFrame;

        public FunctionInvocation(LispFunc func, List<object> args, StackFrame stackFrame)
        {
            Func = func;
            Args = args;
            StackFrame = stackFrame;
        }

        public List<object> EvalArgs(int startIndex)
        {
            var result = new List<object>(Args.Count - startIndex);
            for (int i = startIndex; i < Args.Count; i++)
            {
                object arg = Args[i];
                object evalArg = Utils.Eval(StackFrame, arg);
                result.Add(evalArg);
            }

            return result;
        }


        public List<object> EvalArgs()
        {
            return EvalArgs(1);
        }
    }
}