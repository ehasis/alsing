using System;

namespace Alsing
{
    public class TypeCheck<T>
    {
        private readonly Validation<T> item;

        public TypeCheck(Validation<T> item)
        {
            this.item = item;
        }

        public Validation<T> IsType<O>()
        {
            if (!typeof(O).IsAssignableFrom(typeof(T)))
                throw new ArgumentException("Type {0} is not assignable from type {1}".FormatWith(typeof(O).Name,typeof(T).Name),item.ArgName);
            
            return item;
        }
    }
}
