using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Alsing.Lisp.AST;
using Alsing.Lisp.Infrastructure;

namespace Alsing.Lisp.Runtime
{
    public delegate T OperatorDelegate<T>(T a, T b);

    public class CoreConfigurator
    {
        public static void Configure(RuntimeStack stack)
        {
            //known types
            stack.StackFrame.Scope.SetSymbolValue("Form", typeof (Form));
            stack.StackFrame.Scope.SetSymbolValue("Button", typeof (Button));
            stack.StackFrame.Scope.SetSymbolValue("int", typeof (int));
            stack.StackFrame.Scope.SetSymbolValue("big", typeof (BigInteger));
            stack.StackFrame.Scope.SetSymbolValue("long", typeof (long));
            stack.StackFrame.Scope.SetSymbolValue("bool", typeof (bool));
            stack.StackFrame.Scope.SetSymbolValue("string", typeof (string));
            stack.StackFrame.Scope.SetSymbolValue("char", typeof (char));
            stack.StackFrame.Scope.SetSymbolValue("double", typeof (double));
            stack.StackFrame.Scope.SetSymbolValue("float", typeof (float));
            stack.StackFrame.Scope.SetSymbolValue("date-time", typeof (DateTime));
            stack.StackFrame.Scope.SetSymbolValue("Console", typeof (Console));

            //atoms
            stack.StackFrame.Scope.SetSymbolValue("null", null);
            stack.StackFrame.Scope.SetSymbolValue("true", true);
            stack.StackFrame.Scope.SetSymbolValue("false", false);

            //functions
            stack.StackFrame.Scope.SetSymbolValue("progn", Functions.Progn);
            stack.StackFrame.Scope.SetSymbolValue("concat", Functions.Concat);
            stack.StackFrame.Scope.SetSymbolValue("format", Functions.Format);

            stack.StackFrame.Scope.SetSymbolValue("+", Functions.Add);
            stack.StackFrame.Scope.SetSymbolValue("-", Functions.Sub);
            stack.StackFrame.Scope.SetSymbolValue("*", Functions.Mul);
            stack.StackFrame.Scope.SetSymbolValue("/", Functions.Div);
            stack.StackFrame.Scope.SetSymbolValue("||", Functions.LogicalOr);
            stack.StackFrame.Scope.SetSymbolValue("==", Functions.Equal);
            stack.StackFrame.Scope.SetSymbolValue("!=", Functions.NotEqual);
            stack.StackFrame.Scope.SetSymbolValue("is", Functions.Is);
            stack.StackFrame.Scope.SetSymbolValue(">", Functions.GreaterThan);
            stack.StackFrame.Scope.SetSymbolValue("<", Functions.LessThan);
            stack.StackFrame.Scope.SetSymbolValue(">=", Functions.GreaterOrEqualTo);
            stack.StackFrame.Scope.SetSymbolValue("<=", Functions.LessOrEqualTo);

            stack.StackFrame.Scope.SetSymbolValue("break-debugger", Functions.BreakDebugger);
            stack.StackFrame.Scope.SetSymbolValue("if", Functions.If);
            stack.StackFrame.Scope.SetSymbolValue("#", Functions.EvalFormula);
            stack.StackFrame.Scope.SetSymbolValue("arr", Functions.Arr);
            stack.StackFrame.Scope.SetSymbolValue("list", Functions.List);
            stack.StackFrame.Scope.SetSymbolValue("hash", Functions.Hash);
            stack.StackFrame.Scope.SetSymbolValue("while", Functions.While);
            stack.StackFrame.Scope.SetSymbolValue("foreach", Functions.ForEach);
            stack.StackFrame.Scope.SetSymbolValue("defun", Functions.Defun);
            stack.StackFrame.Scope.SetSymbolValue("defmacro", Functions.Defmacro);
            stack.StackFrame.Scope.SetSymbolValue("lambda", Functions.Lambda);
            stack.StackFrame.Scope.SetSymbolValue("force", Functions.EnsureNotLazy);
            stack.StackFrame.Scope.SetSymbolValue("delay", Functions.Delay);
            stack.StackFrame.Scope.SetSymbolValue("tail", Functions.Tail);
            stack.StackFrame.Scope.SetSymbolValue("setf", Functions.Setf);
            stack.StackFrame.Scope.SetSymbolValue("let", Functions.Let);
            stack.StackFrame.Scope.SetSymbolValue("new-thread", Functions.NewThread);
            stack.StackFrame.Scope.SetSymbolValue("eval-string", Functions.EvalString);
            stack.StackFrame.Scope.SetSymbolValue("eval", Functions.EvalList);
            stack.StackFrame.Scope.SetSymbolValue("include", Functions.Include);
            stack.StackFrame.Scope.SetSymbolValue("reverse", Functions.Reverse);
            stack.StackFrame.Scope.SetSymbolValue("car", Functions.Car);
            stack.StackFrame.Scope.SetSymbolValue("cdr", Functions.Cdr);
            stack.StackFrame.Scope.SetSymbolValue("empty?", Functions.IsEmpty);
            stack.StackFrame.Scope.SetSymbolValue("cast", Functions.Cast);


            stack.StackFrame.Scope.SetSymbolValue("print", call =>
                                                           {
                                                               call.EvalArgs().ToList().ForEach(
                                                                   arg =>
                                                                   stack.Engine.OnPrint(arg == null
                                                                                            ? ""
                                                                                            : arg.ToString()));
                                                               return null;
                                                           });

            stack.StackFrame.Scope.SetSymbolValue("quote", call =>
                                                           {
                                                               var info = new CloneInfo();
                                                               info.StackFrame = call.StackFrame;
                                                               info.LocalIdentifiers = new List<string>();
                                                               info.BackQuote = true;

                                                               object expression = call.Args[1];
                                                               object clone = Utils.Clone(info, expression);
                                                               return clone;
                                                           });

            stack.StackFrame.Scope.SetSymbolValue("operator-prio", call =>
                                                                   {
                                                                       var func =
                                                                           Utils.Eval(call.StackFrame, call.Args[1]) as
                                                                           LispFunc;
                                                                       var prio =
                                                                           (int)
                                                                           Utils.Eval(call.StackFrame, call.Args[2]);

                                                                       if (prio == 0)
                                                                       {
                                                                           //remove
                                                                           stack.OperatorPriority.Remove(func);
                                                                       }
                                                                       else
                                                                       {
                                                                           stack.OperatorPriority[func] = prio;
                                                                       }

                                                                       return null;
                                                                   });


            stack.StackFrame.Scope.SetSymbolValue("input", call =>
                                                           {
                                                               var var = call.Args[1] as SymbolNode;
                                                               string res = Console.ReadLine();
                                                               var.GetSymbol(call.StackFrame).Value = res;
                                                               return null;
                                                           });

            stack.StackFrame.Scope.SetSymbolValue("new", call =>
                                                         {
                                                             var typeId = call.Args[1] as SymbolNode;
                                                             var type = typeId.GetSymbol(call.StackFrame).Value as Type;
                                                             object res = Activator.CreateInstance(type);

                                                             return res;
                                                         });

            stack.StackFrame.Scope.SetSymbolValue("call-by-name", call =>
                                                                  {
                                                                      string methodName =
                                                                          Utils.Eval(call.StackFrame, call.Args[2]).
                                                                              ToString();
                                                                      return stack.HandleCall(call, methodName);
                                                                  });

            stack.StackFrame.Scope.SetSymbolValue("application-run", call =>
                                                                     {
                                                                         Application.Run();
                                                                         return null;
                                                                     });
        }

        //private LispFunc MakeCurryCall(FunctionInvocation call, Cons definedArgs, Cons definedBody, FunctionInvocation kall)
        //{
        //    //clone the args
        //    CloneInfo ci = new CloneInfo();
        //    ci.StackFrame = call.StackFrame;
        //    ci.LocalIdentifiers = new List<string>();

        //    List<ValueNode> argsClone = kall.Args.Select(arg => arg.Clone(ci)).ToList();


        //    LispFunc curryFunc = curryCall =>
        //    {
        //        argsClone.AddRange(curryCall.Args.GetRange(1, curryCall.Args.Count - 1));
        //        FunctionInvocation finalCall = new FunctionInvocation(call.Func, argsClone, curryCall.StackFrame);

        //        if (finalCall.Args.Count - 1 < definedArgs.Args.Count)
        //        {
        //            return MakeCurryCall(kall, definedArgs, definedBody, finalCall);
        //        }
        //        else
        //        {
        //            SetupArgs(definedArgs, finalCall);

        //            //call the body
        //            object res = definedBody.Eval(finalCall.StackFrame);

        //            TearDownArgs(definedArgs, finalCall);
        //            return res;
        //        }
        //    };
        //    return curryFunc;
        //}
    }
}