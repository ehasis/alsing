using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsing.Core.Tests.Extensions.Validation
{
    [TestClass]
    public class StringValidationExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailNotEmpty()
        {
            string test = "";

            test.Require("test")
                .NotEmpty();
        }

        [TestMethod]
        public void PassNotEmpty()
        {
            string test = "Roger";

            test.Require("test")
                .NotEmpty();
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailShorterThan()
        {
            string test = "Roger";
            
            test.Require("test")
                .ShorterThan(3);            
        }

        [TestMethod]
        public void PassShorterThan()
        {
            string test = "Roger";

            //should not fail
            test.Require("test")
                .ShorterThan(7);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailLongerThan()
        {
            string test = "Roger";

            test.Require("test")
                .LongerThan(7);
        }

        [TestMethod]
        public void PassLongerThan()
        {
            string test = "Roger";

            //should not fail
            test.Require("test")
                .LongerThan(3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailStartsWith()
        {
            string test = "Roger";

            test.Require("test")
                .StartsWith("Alsing");
        }

        [TestMethod]
        public void PassStartsWith()
        {
            string test = "Roger";

            //should not fail
            test.Require("test")
                .StartsWith("Rog");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FailExactLength()
        {
            string test = "Roger";

            test.Require("test")
                .ExactLenght(7);
        }

        [TestMethod]
        public void PassExactLength()
        {
            string test = "Roger";

            //should not fail
            test.Require("test")
                .ExactLenght(5);
        }
    }
}
