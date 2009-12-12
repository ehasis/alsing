using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Alsing.Lisp.Infrastructure;
using Alsing.Lisp.Runtime;

namespace Alsing.Lisp.AST
{
    public class ConsNode : ValueNode, IEnumerable
    {
        public List<object> Args { get; set; }

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            foreach (object arg in Args)
            {
                yield return arg;
            }
        }

        #endregion

        public override string ToString()
        {
            if (Args.Count == 0)
                return "()";

            string args =
                Args.Select(arg => string.Format("{0}", (object) arg)).Aggregate((left, right) => left + " " + right);
            string s = string.Format("({0})", args);
            return s;
        }

        public override object Eval(StackFrame stackFrame)
        {
            if (Args.Count == 0)
                return null;

            object res = null;
            object first = Args[0];
            var func = Utils.Eval(stackFrame, first) as LispFunc;
            if (func != null)
            {
                string name = "";
                if (first is SymbolNode)
                {
                    name = ((SymbolNode) first).Name;
                }


                var newStackFrame = new StackFrame(stackFrame, this);
                newStackFrame.Function = func;
                newStackFrame.FunctionName = name;
                Stack.Engine.OnBeginNotifyCall(newStackFrame, this, func, name);

                //function
                var invocation = new FunctionInvocation(func, Args, newStackFrame);

                try
                {
                    res = func(invocation);
                }
                catch (Exception x)
                {
                    throw newStackFrame.CreateException(x.Message);
                }

                Stack.Engine.OnEndNotifyCall(newStackFrame, this, func, name, res);

                //int recursions = 0;
                //while (res is TailRecursionValue)
                //{
                //    TailRecursionValue tail = res as TailRecursionValue;

                //    if (!tail.ReadyForEval)
                //    {
                //        tail.ReadyForEval = true;
                //        break;
                //    }
                //    else
                //    {
                //        recursions++;


                //        ConsNode tailCons = tail.Expression as ConsNode;

                //        newStackFrame = new StackFrame(stackFrame, this);
                //        newStackFrame.Function = func;
                //        newStackFrame.FunctionName = name;
                //        Stack.Engine.OnBeginNotifyCall(newStackFrame, this, func, name);
                //        invocation.Args = tailCons.Args;
                //        try
                //        {
                //            res = func(invocation);
                //        }
                //        catch (Exception x)
                //        {
                //            throw newStackFrame.CreateException(x.Message);
                //        }

                //        Stack.Engine.OnEndNotifyCall(newStackFrame, this, func, name, res);
                //    }
                //}

                return res;
            }


            throw stackFrame.CreateException(string.Format("Invalid function : {0}", this));
        }

        public override object Clone(CloneInfo info)
        {
            if (info.BackQuote)
            {
                var clone = new ConsNode();
                BaseClone(clone);
                clone.Args = new List<object>(10);
                foreach (object arg in Args)
                {
                    object cloneArg = Utils.Clone(info, arg);
                    if (cloneArg is Splice)
                    {
                        var splice = cloneArg as Splice;
                        foreach (object spliceArg in splice.List)
                        {
                            object cloneSpliceArg = Utils.Clone(info, spliceArg);
                            clone.Args.Add(cloneSpliceArg);
                        }
                    }
                    else
                    {
                        clone.Args.Add(cloneArg);
                    }
                }

                return clone;
            }
            else
            {
                var clone = new ConsNode();
                BaseClone(clone);
                clone.Args = new List<object>(10);
                foreach (object arg in Args)
                {
                    object cloneArg = Utils.Clone(info, arg);
                    clone.Args.Add(cloneArg);
                }

                return clone;
            }
        }
    }
}