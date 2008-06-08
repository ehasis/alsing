using System;
using System.Linq;
using Alsing.Lisp.AST;
using Alsing.Lisp.Infrastructure;

namespace Alsing.Lisp.Runtime
{
    public static class Utils
    {
        public static object Force(object item)
        {
            if (item is LazyValue)
            {
                var lRes = item as LazyValue;
                item = lRes.Eval();
            }

            return item;
        }

        public static object Eval(StackFrame stackFrame, object arg)
        {
            if (arg is ValueNode)
                return ((ValueNode) arg).Eval(stackFrame);
            else
                return arg;
        }

        public static object Clone(CloneInfo info, object arg)
        {
            if (arg is ValueNode)
                return ((ValueNode) arg).Clone(info);
            else
                return arg;
        }

        public static bool IsTrue(object condition)
        {
            if (condition == null)
                return false;

            if (condition is bool)
            {
                return (bool) condition;
            }

            if (condition is int)
            {
                return 0 != (int) condition;
            }

            if (condition is double)
            {
                return 0.0 != (double) condition;
            }

            return true;
        }

        public static ConsNode NewCons(RuntimeStack stack)
        {
            var resNode = new ConsNode();
            resNode.HideFromCallstack = true;
            resNode.Stack = stack;
            return resNode;
        }


        public static void SetupMacroArgs(ConsNode definedArgs, FunctionInvocation call, Scope newScope)
        {
            int i = 1;

            //todo: verify arg list, only identifiers allowed

            //setup local args
            for (int argIndex = 0; argIndex < definedArgs.Args.Count; argIndex++)
            {
                var arg = definedArgs.Args[argIndex] as SymbolNode;

                string argName = arg.Name;

                if (argName == "&optional") {}
                else if (argName == "&rest")
                {
                    argIndex++;
                    var restArg = definedArgs.Args[argIndex] as SymbolNode;
                    string restName = restArg.Name;

                    ConsNode restCons = NewCons(call.StackFrame.Root);
                    restCons.Args = call.Args.Skip(i).ToList();

                    newScope.PushSymbol(restName).Value = restCons;
                }
                else
                {
                    Symbol symbol = newScope.PushSymbol(argName);
                    object bodyArg = call.Args[i];
                    symbol.Value = bodyArg;
                }
                i++;
            }
        }


        public static void SetupArgs(ConsNode definedArgs, FunctionInvocation call, Scope newScope)
        {
            int i = 1;

            //todo: verify arg list, only identifiers allowed

            //setup local args
            for (int argIndex = 0; argIndex < definedArgs.Args.Count; argIndex ++)
            {
                var arg = definedArgs.Args[argIndex] as SymbolNode;

                string argName = arg.Name;

                if (argName == "&optional") {}
                else if (argName == "&rest")
                {
                    argIndex ++;
                    var restArg = definedArgs.Args[argIndex] as SymbolNode;
                    string restName = restArg.Name;

                    ConsNode restCons = NewCons(call.StackFrame.Root);
                    restCons.Args = call.EvalArgs(i);

                    newScope.PushSymbol(restName).Value = restCons;
                }
                else if (argName[0] == '*')
                {
                    //ref symbol
                    var argId = call.Args[i] as SymbolNode;
                    argName = argName.Substring(1);
                    if (argId == null)
                    {
                        throw new Exception(
                            string.Format("Argument '{0}' is defined as a pointer, but passed as a value", argName));
                    }

                    Symbol sourceVar = argId.GetSymbol(call.StackFrame);

                    newScope.DeclareRef(argName, sourceVar);
                }
                else if (argName[0] == '!')
                {
                    argName = argName.Substring(1);
                    newScope.PushSymbol(argName);
                    var bodyArg = call.Args[i] as ValueNode;
                    newScope.SetSymbolValue(argName, bodyCall => bodyArg.Eval(call.StackFrame));
                }
                else if (argName[0] == '#')
                {
                    argName = argName.Substring(1);
                    Symbol symbol = newScope.PushSymbol(argName);
                    object bodyArg = call.Args[i];
                    symbol.Value = bodyArg;
                }
                else if (argName[0] == ':')
                {
                    var argId = call.Args[i] as SymbolNode;
                    argName = argName.Substring(1);
                    if (argId == null)
                    {
                        throw new Exception(
                            string.Format("Argument '{0}' is defined as a verbatim, but passed as a value", argName));
                    }
                    if (argId.Name != argName)
                    {
                        throw new Exception(string.Format("Argument '{0}' is defined as a verbatim, but passed as {1}",
                                                          argName, argId.Name));
                    }
                }
                else if (argName[0] == '@')
                {
                    var argId = call.Args[i] as SymbolNode;
                    object argValue = argId.Name;
                    argName = argName.Substring(1);
                    newScope.PushSymbol(argName).Value = argValue;
                }
                else
                {
                    //normal var
                    object argValue = Eval(call.StackFrame, call.Args[i]);
                    newScope.PushSymbol(argName).Value = argValue;
                }
                i++;
            }

            //for (int j = i; j < call.Args.Count; j++)
            //{
            //    ValueNode node = call.Args[j];
            //    string argName = string.Format("arg{0}", j);
            //    object argValue = node.GetValue(stack);
            //    stack.PushSymbol(argName);
            //    stack.Scope.SetSymbolValue(argName, argValue);
            //}
        }

        public static void TearDownArgs(ConsNode definedArgs, FunctionInvocation call)
        {
            //int i = 1;
            ////tear down args
            //foreach (IdentifierNode arg in definedArgs.Args)
            //{
            //    string argName = arg.Name;

            //    if (argName.StartsWith("*"))
            //    {
            //        argName = argName.Substring(1);
            //        call.StackFrame.PopSymbol(argName);
            //    }
            //    else if (argName.StartsWith("!"))
            //    {
            //        argName = argName.Substring(1);
            //        call.StackFrame.PopSymbol(argName);
            //    }
            //    else if (argName.StartsWith("#"))
            //    {
            //        argName = argName.Substring(1);
            //        call.StackFrame.PopSymbol(argName);
            //    }
            //    else if (argName.StartsWith("@"))
            //    {
            //        argName = argName.Substring(1);
            //        call.StackFrame.PopSymbol(argName);
            //    }
            //    else if (argName.StartsWith(":"))
            //    {

            //    }
            //    else
            //    {
            //        call.StackFrame.PopSymbol(argName);
            //    }


            //    i++;
            //}

            ////for (int j = i; j < call.Args.Count; j++)
            ////{
            ////    ValueNode node = call.Args[j];
            ////    string argName = string.Format("arg{0}", j);
            ////    stack.PopSymbol(argName);
            ////}
        }

        public static string CleanName(string argName)
        {
            if (argName.StartsWith("*"))
                return argName.Substring(1);
            if (argName.StartsWith("!"))
                return argName.Substring(1);
            if (argName.StartsWith(":"))
                return argName.Substring(1);
            if (argName.StartsWith("@"))
                return argName.Substring(1);

            return argName;
        }
    }
}