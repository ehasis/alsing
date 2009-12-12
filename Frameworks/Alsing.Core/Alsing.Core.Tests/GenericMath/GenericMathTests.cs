using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alsing.Core.Tests.GenericMath
{
    [TestClass]
    public class GenericMathTests
    {

        [TestMethod]
        public void CalculateWithDouble()
        {
            double a = 4;
            double b = 2;

            double addRes = Add(a, b);
            Assert.AreEqual(6, addRes);

            double subRes = Sub(a, b);
            Assert.AreEqual(2, subRes);

            double mulRes = Mul(a, b);
            Assert.AreEqual(8, mulRes);

            double divRes = Div(a, b);
            Assert.AreEqual(2, divRes);            
        }

        [TestMethod]
        public void CalculateWithInt()
        {
            int a = 4;
            int b = 2;

            int addRes = Add(a, b);
            Assert.AreEqual(6, addRes);

            int subRes = Sub(a, b);
            Assert.AreEqual(2, subRes);

            int mulRes = Mul(a, b);
            Assert.AreEqual(8, mulRes);

            int divRes = Div(a, b);
            Assert.AreEqual(2, divRes);
        }

        [TestMethod]
        public void CalculateWithDecimal()
        {
            decimal a = 4;
            decimal b = 2;

            decimal addRes = Add(a, b);
            Assert.AreEqual(6, addRes);

            decimal subRes = Sub(a, b);
            Assert.AreEqual(2, subRes);

            decimal mulRes = Mul(a, b);
            Assert.AreEqual(8, mulRes);

            decimal divRes = Div(a, b);
            Assert.AreEqual(2, divRes);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CalculateWithString()
        {
            string a = "Roger";
            string b = "Alsing";

            //should throw
            string addRes = Add(a, b);
        }

        private static T Add<T>(T a,T b)
        {
            Numeric<T> na = a;
            Numeric<T> nb = b;

            return na + nb;
        }

        private static T Sub<T>(T a, T b)
        {
            Numeric<T> na = a;
            Numeric<T> nb = b;

            return na - nb;
        }

        private static T Mul<T>(T a, T b)
        {
            Numeric<T> na = a;
            Numeric<T> nb = b;

            return na * nb;
        }

        private static T Div<T>(T a, T b)
        {
            Numeric<T> na = a;
            Numeric<T> nb = b;

            return na / nb;
        }
    }
}
