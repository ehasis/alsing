namespace QI4N.Framework
{
    using System;

    public class ConcernsAttribute : Attribute
    {
        public ConcernsAttribute(params Type[] concernTypes)
        {
            this.ConcernTypes = concernTypes;
        }

        public Type[] ConcernTypes { get; private set; }
    }


}