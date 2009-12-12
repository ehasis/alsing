using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsing.Core.Tests.Extensions
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void FormatAsTest()
        {
            string format = ">{0}<";

            Assert.AreEqual(">1<", 1.FormatAs(format));
            Assert.AreEqual(">True<", true.FormatAs(format));
        }

        [TestMethod]
        public void FormatWithTest()
        {
            string format = ">{0}<";

            Assert.AreEqual(">1<", format.FormatWith(1));
            Assert.AreEqual(">True<", format.FormatWith(true));
        }

        [TestMethod]
        public void ToProperCaseTest()
        {
            string text = "ROGER ALSING";

            Assert.AreEqual("Roger Alsing", text.ToProperCase());
        }

        [TestMethod]
        public void LikeTest()
        {
            Assert.IsTrue("Roger Alsing".Like("%oger ?lsin%"));
        }
    }
}
