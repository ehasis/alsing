using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsing.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsing.Core.Tests.Threading
{
    [TestClass]
    public class ForkTests
    {
        [TestMethod]
        public void ForkCall()
        {
            //Ok I don't quite know how to test if the execution was async

            bool var1 = false;
            bool var2 = false;

            Fork.Begin()
                   .Call(() => var1 = true)
                   .Call(() => var2 = true)
                   .End();

            Assert.IsTrue(var1);
            Assert.IsTrue(var2);
        }
    }
}
