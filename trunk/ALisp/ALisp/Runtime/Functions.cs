using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Alsing.Lisp.AST;
using Alsing.Lisp.Infrastructure;

namespace Alsing.Lisp.Runtime
{
    public class Functions
    {
        private static int gensymId;

        public static object NewThread(FunctionInvocation call)
        {
            var func = Utils.Eval(call.StackFrame, call.Args[1]) as LispFunc;
            var args = new List<object>();
            args.Add(new SymbolNode());
            var kall = new FunctionInvocation(func, args, call.StackFrame.Root.StackFrame);

            var thread = new Thread(() => func(kall));
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Lowest;

            return thread;
        }

        public static object EvalString(FunctionInvocation call)
        {
            var code = Utils.Eval(call.StackFrame, call.Args[1]) as string;
            object res = call.StackFrame.Root.Engine.EvaluateString(code, "runtime");

            return res;
        }

        public static object EvalList(FunctionInvocation call)
        {
            var list = Utils.Eval(call.StackFrame, call.Args[1]) as ConsNode;
            object res = Utils.Eval(call.StackFrame, list);
            return res;
        }

        public static object Include(FunctionInvocation call)
        {
            var path = Utils.Eval(call.StackFrame, call.Args[1]) as string;
            string code = "";
            using (var sr = new StreamReader(path, Encoding.Default))
            {
                code = sr.ReadToEnd();
            }

            return call.StackFrame.Root.Engine.EvaluateString(code, path);
        }

        public static object Reverse(FunctionInvocation call)
        {
            var list = Utils.Eval(call.StackFrame, call.Args[1]) as IEnumerable;

            var res = new List<object>();
            foreach (object item in list)
            {
                res.Add(item);
            }

            res.Reverse();

            ConsNode resNode = Utils.NewCons(call.StackFrame.Root);
            resNode.Args = res;
            return resNode;
        }

        public static object Car(FunctionInvocation call)
        {
            var list = Utils.Eval(call.StackFrame, call.Args[1]) as IEnumerable;

            foreach (object item in list)
            {
                return item;
            }

            return null; //list was empty
        }

        public static object Cdr(FunctionInvocation call)
        {
            var list = Utils.Eval(call.StackFrame, call.Args[1]) as IEnumerable;

            bool first = true;
            var res = new List<object>();
            foreach (object item in list)
            {
                if (first)
                    first = false;
                else
                    res.Add(item);
            }

            ConsNode resNode = Utils.NewCons(call.StackFrame.Root);
            resNode.Args = res;
            return resNode;
        }


        public static object IsEmpty(FunctionInvocation call)
        {
            var list = Utils.Eval(call.StackFrame, call.Args[1]) as IEnumerable;
            foreach (object item in list)
                return false;

            return true;
        }

        public static object Progn(FunctionInvocation call)
        {
            object res = null;
            string funcName = "progn";
            call.Args.GetRange(1, call.Args.Count - 1).ForEach(arg => res = Utils.Eval(call.StackFrame, arg));
            return res;
        }

        public static object Gensym(FunctionInvocation call)
        {
            gensymId++;

            return string.Format("sym{0}", gensymId);
        }

        public static object Lambda(FunctionInvocation call)
        {
            ConsNode definedArgs = null;
            ConsNode definedBody = null;

            var info = new CloneInfo();
            info.StackFrame = call.StackFrame;
            Scope creatorScope = call.StackFrame.Scope;


            ValueNode bodyNode = null;
            if (call.Args.Count == 2)
            {
                definedArgs = new ConsNode();
                definedArgs.Args = new List<object>();
                var varNode = new SymbolNode();
                varNode.Name = "item";
                definedArgs.Args.Add(varNode);
                bodyNode = call.Args[1] as ConsNode;
            }
            else if (call.Args.Count == 3)
            {
                if (call.Args[1] is ConsNode)
                    definedArgs = call.Args[1] as ConsNode;
                else
                    definedArgs = Utils.Eval(call.StackFrame, call.Args[1]) as ConsNode;

                bodyNode = call.Args[2] as ConsNode;
            }

            info.LocalIdentifiers =
                definedArgs.Args.Cast<SymbolNode>().Select(arg => Utils.CleanName(arg.Name)).ToList();

            //make snapshot of bound non local identifiers
            definedBody = bodyNode.Clone(info) as ConsNode;

            LispFunc lispFunc = kall =>
                                {
                                    //todo: verify arg list, only identifiers allowed
                                    Scope oldScope = kall.StackFrame.Scope;
                                    var newScope = new Scope(creatorScope);
                                    Utils.SetupArgs(definedArgs, kall, newScope);
                                    kall.StackFrame.Scope = newScope;


                                    //call the body
                                    object res = definedBody.Eval(kall.StackFrame);


                                    Utils.TearDownArgs(definedArgs, kall);
                                    kall.StackFrame.Scope = oldScope;

                                    return res;
                                };

            return lispFunc;
        }

        public static object Setf(FunctionInvocation call)
        {
            string varName = "";
            if (call.Args[1] is SymbolNode)
            {
                var var = call.Args[1] as SymbolNode;
                varName = var.Name;
            }
            else
            {
                object tmp = Utils.Eval(call.StackFrame, call.Args[1]);
                varName = tmp as string;
                if (varName == null)
                {
                    throw call.StackFrame.CreateException("Symbol names must be strings");
                }
            }

            object val = call.Args[2];
            object res = Utils.Eval(call.StackFrame, val);
            call.StackFrame.Scope.GetSymbol(varName).Value = res;
            call.StackFrame.Root.StackFrame.Scope.GetSymbol("?").Value = res;
            return null;
        }

        public static object Let(FunctionInvocation call)
        {
            string varName = "";
            if (call.Args[1] is SymbolNode)
            {
                var var = call.Args[1] as SymbolNode;
                varName = var.Name;
            }
            else
            {
                object tmp = Utils.Eval(call.StackFrame, call.Args[1]);
                varName = tmp as string;
                if (varName == null)
                {
                    throw call.StackFrame.CreateException("Symbol names must be strings");
                }
            }


            var val = call.Args[2] as ValueNode;
            Symbol symbol = call.StackFrame.Scope.PushSymbol(varName);
            symbol.Value = val.Eval(call.StackFrame);


            var body = call.Args[3] as ValueNode;
            object res = body.Eval(call.StackFrame);
            call.StackFrame.Scope.PopSymbol(varName);

            return res;
        }

        public static object Defun(FunctionInvocation call)
        {
            var func = call.Args[1] as SymbolNode;
            var definedArgs = call.Args[2] as ConsNode;
            //ConsNode definedBody = call.Args[3] as ConsNode;

            ConsNode definedBody = WrapInProgn(call.Args, 3, call.StackFrame.Root);

            Scope creatorScope = call.StackFrame.Scope;

            LispFunc lispFunc = kall =>
                                {
                                    //if (kall.Args.Count - 1 < definedArgs.Args.Count)
                                    //{
                                    //    LispFunc curryFunc = MakeCurryCall(call, definedArgs, definedBody, kall);

                                    //    return curryFunc;                    
                                    //}
                                    //else
                                    //{
                                    Scope oldScope = kall.StackFrame.Scope;
                                    var newScope = new Scope(creatorScope);
                                    Utils.SetupArgs(definedArgs, kall, newScope);
                                    kall.StackFrame.Scope = newScope;

                                    //call the body
                                    object res = definedBody.Eval(kall.StackFrame);

                                    //tail recursion
                                    int recursions = 0;
                                    while (res is TailRecursionValue)
                                    {
                                        recursions++;

                                        var tail = res as TailRecursionValue;
                                        var tailCons = tail.Expression as ConsNode;

                                        newScope = new Scope(creatorScope);
                                        kall.Args = tailCons.Args;
                                        Utils.SetupArgs(definedArgs, kall, newScope);
                                        kall.StackFrame.Scope = newScope;


                                        res = definedBody.Eval(kall.StackFrame);
                                    }

                                    Utils.TearDownArgs(definedArgs, kall);
                                    kall.StackFrame.Scope = oldScope;

                                    return res;
                                    //}
                                };

            call.StackFrame.Scope.SetSymbolValue(func.Name, lispFunc);
            //      FunctionMeta meta = call.StackFrame.Root.GetFunctionMeta(lispFunc);
            //      meta.ParameterCount = definedArgs.Args.Count;

            return null;
        }

        private static ConsNode WrapInProgn(List<object> args, int startIndex, RuntimeStack stack)
        {
            var definedBody = new ConsNode();
            definedBody.Stack = stack;
            definedBody.HideFromCallstack = true;
            definedBody.Code = "";
            definedBody.CodeName = "runtime";
            definedBody.Args = new List<object>();

            var progn = new SymbolNode();
            progn.Name = "progn";
            progn.Stack = stack;
            progn.HideFromCallstack = true;
            progn.Code = "";
            progn.CodeName = "runtime";

            definedBody.Args.Add(progn);
            definedBody.Args.AddRange(args.GetRange(startIndex, args.Count - startIndex));
            return definedBody;
        }

        public static object List(FunctionInvocation call)
        {
            List<object> res = call.EvalArgs().ToList();
            return res;
        }

        public static object Hash(FunctionInvocation call)
        {
            var res = new Hashtable();

            foreach (ConsNode node in call.Args.GetRange(1, call.Args.Count - 1))
            {
                object key = Utils.Eval(call.StackFrame, call.Args[0]);
                object value = Utils.Eval(call.StackFrame, call.Args[1]);
                res[key] = value;
            }
            return res;
        }

        public static object While(FunctionInvocation call)
        {
            var condition = (bool) Utils.Eval(call.StackFrame, call.Args[1]);

            while (condition)
            {
                for (int i = 2; i < call.Args.Count; i++)
                {
                    Utils.Eval(call.StackFrame, call.Args[i]);
                }
                condition = (bool) Utils.Eval(call.StackFrame, call.Args[1]);
            }

            return null;
        }

        public static object ForEach(FunctionInvocation call)
        {
            var var = call.Args[1] as SymbolNode;
            object list = call.Args[2];
            var body = call.Args[3] as ValueNode;

            var items = Utils.Eval(call.StackFrame, list) as IEnumerable;

            if (items == null)
                throw call.StackFrame.CreateException("The source is not an IEnumerable");


            foreach (object item in items)
            {
                var.GetSymbol(call.StackFrame).Value = item;
                body.Eval(call.StackFrame);
            }

            return null;
        }

        public static object Defmacro(FunctionInvocation call)
        {
            var func = call.Args[1] as SymbolNode;
            var definedArgs = call.Args[2] as ConsNode;
            var definedBody = call.Args[3] as ConsNode;

            LispFunc lispFunc = kall =>
                                {
                                    //---kall.StackFrame.ScopeId = Guid.NewGuid();---
                                    //nope. stay in the same scope
                                    Scope oldScope = kall.StackFrame.Scope;
                                    var newScope = new Scope(kall.StackFrame.Scope);
                                    Utils.SetupMacroArgs(definedArgs, kall, newScope);
                                    kall.StackFrame.Scope = newScope;

                                    //call the body
                                    object tmpBody = definedBody.Eval(kall.StackFrame);
                                    object res = Utils.Eval(kall.StackFrame, tmpBody);


                                    Utils.TearDownArgs(definedArgs, kall);
                                    kall.StackFrame.Scope = oldScope;

                                    return res;
                                };

            call.StackFrame.Scope.SetSymbolValue(func.Name, lispFunc);

            return null;
        }

        public static object EnsureNotLazy(FunctionInvocation call)
        {
            object res = Utils.Eval(call.StackFrame, call.Args[1]);
            res = Utils.Force(res);

            return res;
        }

        public static object EvalFormula(FunctionInvocation call)
        {
            bool dirty = true;

            var tmpNode = new ConsNode();
            tmpNode.Args = call.Args.GetRange(1, call.Args.Count - 1);
            tmpNode.Stack = call.StackFrame.Root;
            tmpNode.CodeName = "runtime";


            while (dirty)
            {
                int bestPrio = 0;
                SymbolNode bestNode = null;
                int bestIndex = 0;
                for (int i = 1; i < tmpNode.Args.Count; i++)
                {
                    var operatorId = tmpNode.Args[i] as SymbolNode;
                    if (operatorId != null)
                    {
                        var func = operatorId.Eval(call.StackFrame) as LispFunc;
                        if (func == null)
                            continue;

                        if (call.StackFrame.Root.OperatorPriority.ContainsKey(func))
                        {
                            int operatorPrio = call.StackFrame.Root.OperatorPriority[func];
                            if (bestNode == null || operatorPrio > bestPrio)
                            {
                                bestPrio = operatorPrio;
                                bestNode = operatorId;
                                bestIndex = i;
                            }
                        }
                    }
                }

                if (bestNode != null)
                {
                    object left = tmpNode.Args[bestIndex - 1];
                    object right = tmpNode.Args[bestIndex + 1];
                    var op = tmpNode.Args[bestIndex] as SymbolNode;

                    if (tmpNode.Args.Count == 3)
                    {
                        tmpNode.Args.Clear();
                        tmpNode.Args.Add(op);
                        tmpNode.Args.Add(left);
                        tmpNode.Args.Add(right);
                    }
                    else
                    {
                        var newNode = new ConsNode();
                        newNode.Code = op.Code;
                        newNode.CodeStartIndex = op.CodeStartIndex - 1;
                        newNode.CodeLength = op.CodeLength;
                        newNode.CodeName = "runtime";
                        newNode.HideFromCallstack = op.HideFromCallstack;

                        newNode.Args = new List<object>();
                        newNode.Args.Add(op);
                        newNode.Args.Add(left);
                        newNode.Args.Add(right);
                        newNode.Stack = call.StackFrame.Root;

                        tmpNode.Args.RemoveRange(bestIndex - 1, 3);
                        tmpNode.Args.Insert(bestIndex - 1, newNode);
                    }
                }
                else if (bestNode == null)
                {
                    dirty = false;
                }
            }

            return tmpNode.Eval(call.StackFrame);
        }

        public static object BreakDebugger(FunctionInvocation call)
        {
            Debugger.Break();
            return null;
        }

        public static object Arr(FunctionInvocation call)
        {
            object[] res = call.EvalArgs().ToArray();
            return res;
        }


        public static object Concat(FunctionInvocation call)
        {
            return
                call.EvalArgs().Select(item => item == null ? "" : item.ToString()).Aggregate(
                    (left, right) => left + right);
        }

        public static object Format(FunctionInvocation call)
        {
            object[] args =
                call.Args.GetRange(2, call.Args.Count - 2).Select(arg => Utils.Eval(call.StackFrame, arg)).ToArray();
            var format = Utils.Eval(call.StackFrame, call.Args[1]) as string;
            string res = string.Format(format, args);

            return res;
        }

        public static object Delay(FunctionInvocation call)
        {
            var ci = new CloneInfo();
            ci.LocalIdentifiers = new List<string>();
            ci.StackFrame = call.StackFrame;

            object clone = Utils.Clone(ci, call.Args[1]);

            var lazy = new LazyValue(() =>
                                     {
                                         object result = Utils.Eval(call.StackFrame, clone);
                                         return Utils.Force(result);
                                     });

            return lazy;
        }

        public static object Tail(FunctionInvocation call)
        {
            var ci = new CloneInfo();
            ci.LocalIdentifiers = new List<string>();
            ci.StackFrame = call.StackFrame;

            object clone = Utils.Clone(ci, call.Args[1]);


            var tail = new TailRecursionValue(clone);

            return tail;
        }

        //public static object Tail(FunctionInvocation call)
        //{
        //    CloneInfo ci = new CloneInfo();
        //    ci.LocalIdentifiers = new List<string>();
        //    ci.StackFrame = call.StackFrame;

        //    object clone = Utils.Clone(ci, call.Args[1]);

        //    TailRecursionValue lazy = new TailRecursionValue(() =>
        //    {
        //        object result = Utils.Eval(call.StackFrame, clone);
        //        return Utils.Force(result);
        //    });

        //    return lazy;
        //}


        public static object Add(FunctionInvocation call)
        {
            List<object> args = call.EvalArgs();

            object firstArg = args[0];

            if (firstArg is double)
                return Operation<double>(args, (a, b) => a + b);

            if (firstArg is int)
                return Operation<int>(args, (a, b) => a + b);

            if (firstArg is BigInteger)
                return Operation<BigInteger>(args, (a, b) => a + b);

            return null;
        }

        public static object Sub(FunctionInvocation call)
        {
            List<object> args = call.EvalArgs();

            object firstArg = args[0];

            if (firstArg is double)
                return Operation<double>(args, (a, b) => a - b);

            if (firstArg is int)
                return Operation<int>(args, (a, b) => a - b);

            if (firstArg is BigInteger)
                return Operation<BigInteger>(args, (a, b) => a - b);

            return null;
        }

        public static object Mul(FunctionInvocation call)
        {
            List<object> args = call.EvalArgs();

            object firstArg = args[0];

            if (firstArg is double)
                return Operation<double>(args, (a, b) => a*b);

            if (firstArg is int)
                return Operation<int>(args, (a, b) => a*b);

            if (firstArg is BigInteger)
                return Operation<BigInteger>(args, (a, b) => a*b);

            return null;
        }

        public static object Div(FunctionInvocation call)
        {
            List<object> args = call.EvalArgs();

            object firstArg = args[0];

            if (firstArg is double)
                return Operation<double>(args, (a, b) => a/b);

            if (firstArg is int)
                return Operation<int>(args, (a, b) => a/b);

            if (firstArg is BigInteger)
                return Operation<BigInteger>(args, (a, b) => a/b);

            return null;
        }

        public static object GreaterThan(FunctionInvocation call)
        {
            var left = Utils.Eval(call.StackFrame, call.Args[1]) as IComparable;
            object tmpRight = Utils.Eval(call.StackFrame, call.Args[2]);
            object castRight = Convert.ChangeType(tmpRight, left.GetType());
            var right = castRight as IComparable;

            if (left.CompareTo(right) > 0)
                return true;
            else
                return false;
        }

        public static object GreaterOrEqualTo(FunctionInvocation call)
        {
            var left = Utils.Eval(call.StackFrame, call.Args[1]) as IComparable;
            object tmpRight = Utils.Eval(call.StackFrame, call.Args[2]);
            object castRight = Convert.ChangeType(tmpRight, left.GetType());
            var right = castRight as IComparable;

            if (left.CompareTo(right) >= 0)
                return true;
            else
                return false;
        }

        public static object LessThan(FunctionInvocation call)
        {
            var left = Utils.Eval(call.StackFrame, call.Args[1]) as IComparable;
            object tmpRight = Utils.Eval(call.StackFrame, call.Args[2]);
            object castRight = Convert.ChangeType(tmpRight, left.GetType());
            var right = castRight as IComparable;

            if (left.CompareTo(right) < 0)
                return true;
            else
                return false;
        }

        public static object LessOrEqualTo(FunctionInvocation call)
        {
            var left = Utils.Eval(call.StackFrame, call.Args[1]) as IComparable;
            object tmpRight = Utils.Eval(call.StackFrame, call.Args[2]);
            object castRight = Convert.ChangeType(tmpRight, left.GetType());
            var right = castRight as IComparable;

            if (left.CompareTo(right) <= 0)
                return true;
            else
                return false;
        }

        public static object Operation<T>(List<object> args, OperatorDelegate<T> del)
        {
            if (args.Count == 2)
            {
                var left = (T) args[0];
                var right = (T) args[1];

                return del(left, right);
            }
            else
            {
                return args.Cast<T>().Aggregate((left, right) => del(left, right));
            }
        }

        public static object LogicalOr(FunctionInvocation call)
        {
            List<object> args = call.EvalArgs();

            return Operation<bool>(args, (a, b) => a || b);
        }

        public static object Equal(FunctionInvocation call)
        {
            object[] values = call.EvalArgs().ToArray();
            if (values[0] != null)
            {
                object right = Convert.ChangeType(values[1], values[0].GetType());
                return values[0].Equals(right);
            }
            else
            {
                if (values[1] == null)
                    return true;
                else
                    return false;
            }
        }

        public static object NotEqual(FunctionInvocation call)
        {
            return !(bool) Equal(call);
        }

        public static object Is(FunctionInvocation call)
        {
            object target = Utils.Eval(call.StackFrame, call.Args[1]);
            var type = Utils.Eval(call.StackFrame, call.Args[2]) as Type;

            if (type.IsAssignableFrom(target.GetType()))
                return true;
            else
                return false;
        }

        public static object If(FunctionInvocation call)
        {
            object condition = Utils.Eval(call.StackFrame, call.Args[1]);

            if (Utils.IsTrue(condition))
                return Utils.Eval(call.StackFrame, call.Args[2]);
            else
                return Utils.Eval(call.StackFrame, call.Args[3]);
        }

        public static object Cast(FunctionInvocation call)
        {
            object target = Utils.Eval(call.StackFrame, call.Args[1]);
            var type = Utils.Eval(call.StackFrame, call.Args[2]) as Type;

            if (type == typeof (BigInteger))
            {
                if (target is int)
                {
                    var val = (int) target;
                    return new BigInteger(val);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                object res = Convert.ChangeType(target, type);
                return res;
            }
        }
    }
}