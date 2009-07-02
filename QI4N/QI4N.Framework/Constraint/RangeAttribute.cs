namespace QI4N.Framework
{
    using System;

    public class RangeAttribute : ConstraintAttribute
    {
        private readonly object max;

        private readonly object min;

        public RangeAttribute(object min, object max)
        {
            this.min = min;
            this.max = max;
        }

        public override string GetConstraintName()
        {
            return string.Format("Range '{0}'-'{1}'", this.min, this.max);
        }

        public override bool IsValid(object value)
        {
            var cMin = this.min as IComparable;
            var cMax = this.max as IComparable;

            return cMin.CompareTo(value) <= 0 && cMax.CompareTo(value) >= 0;
        }
    }
}