namespace QI4N.Framework.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    public class ParameterConstraintViolationException : Exception
    {
        [DebuggerStepThrough]
        [DebuggerHidden]
        public ParameterConstraintViolationException(Composite composite, MethodInfo method, IEnumerable<ConstraintViolation> violations):base(GetMessage(composite,method,violations))
        {
            
        }

        [DebuggerStepThrough]
        [DebuggerHidden]
        private static string GetMessage(Composite composite, MethodInfo method, IEnumerable<ConstraintViolation> violations)
        {
            string message = string.Format("{0}.{1} caused parameter constraint violations", composite.GetType().Name, method.Name);
            foreach(var violation in violations)
            {
                string violationMessage = string.Format("Parameter: '{0}' Value: '{1}' Constraint: '{2}'",violation.Name,violation.Value ?? "{null}",violation.Constraint);
                message += Environment.NewLine + violationMessage;
            }
            return message;
        }
    }
}