using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication23.Experimental
{
    using QI4N.Framework;

    public class SayHelloConcern : ConcernOf<SayHelloBehavior> , SayHelloBehavior
    {
        #region SayHelloBehavior Members

        public void SayHello()
        {
            Console.WriteLine("This is the SayHello Concern speaking");
            next.SayHello();            
        }

        #endregion
    }
}
