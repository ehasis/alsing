namespace QI4N.Framework.Runtime
{
    using System;
    using System.Reflection;

    public class ParameterConstraintViolationException : Exception
    {
        public ParameterConstraintViolationException(Composite composite, MethodInfo method, object violations)
        {
            throw new NotImplementedException();
        }
    }
}