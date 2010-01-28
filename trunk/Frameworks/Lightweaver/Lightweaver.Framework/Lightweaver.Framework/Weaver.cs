using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Lightweaver.Framework
{
    public class Weaver
    {
        public T CreateProxy<T>(params object[] args)
        {
            return default(T);
        }

        public T CreateProxy<T>(Expression<Func<Weaver,T>> creator)
        {
            creator = ReduceCreator<T>(creator);
            
            NewExpression newExp = creator.Body as NewExpression;

            if (newExp == null)
                throw new NotSupportedException("Creator expressions must start with a 'new' statement");

            var ctor = newExp.Constructor;

            var creatorDelegate = creator.Compile();

            return creatorDelegate(this);
        }

        private static Expression<Func<Weaver, T>> ReduceCreator<T>(Expression<Func<Weaver, T>> creator)
        {
            while (creator.CanReduce)
                creator = (Expression<Func<Weaver, T>>)creator.Reduce();

            return creator;
        }
    }
}
