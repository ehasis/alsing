using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsing.Core.Tests.Extensions.Validation
{
    [TestClass]
    public class ValidationExtensionsTests
    {
            
        [TestMethod]
        public void PassNotNull()
        {
            var o = new object();

            o.Require("o")
                .NotNull();
        }

        [TestMethod]
        public void PassExistsInList()
        {
            var o = new object();
            var list = new List<object>() {o};

            o.Require("o")
                .ExistsInList(list);
        }

        [TestMethod]
        public void PassIsInRange()
        {
            int i = 5;

            i.Require("i")
                .IsInRange(1, 10);
        }

        [TestMethod]
        public void PassIsGreaterThan() 
        {
            int i = 5;

            i.Require("i")
                .IsGreaterThan(1);
        }

        [TestMethod]
        public void PassIsLessThan() 
        {
            int i = 5;

            i.Require("i")
                .IsLessThan(10);
        }

        [TestMethod]
        public void PassIsEqualTo() 
        {
            int i = 5;

            i.Require("i")
                .IsEqualTo(5);
        }

        [TestMethod]
        public void PassTypeCheck()
        {
            int i = 5;

            i.Require("i")
                .TypeCheck()
                .IsType<ValueType>();
        }
    }
}
