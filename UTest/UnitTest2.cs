using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;

namespace UTest
{
    [TestClass]
    public class UnitTest2
    {
        //TestMethod

        [TestCase("Fizz", 3)]
        [TestCase("Fizz", 6)]
        [TestCase("Buzz", 5)]
        [TestCase("Buzz", 10)]
        [TestCase("FizzBuzz", 15)]
        [TestCase("FizzBuzz", 30)]
        [TestCase("", 7)]
        //[TestMethod]
        public void TestFizzBuzz(string expected, int number)
        //public void TestFizzBuzz()
        {
            NUnit.Framework.Assert.AreEqual(expected, FizzBuzz(number));
            //Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual("Fizz", FizzBuzz(3));
        }

        private string FizzBuzz(int number)
        {
            if (number % 3 == 0 && number % 5 == 0)
                return "FizzBuzz";
            if (number % 3 == 0)
                return "Fizz";
            if (number % 5 == 0)
                return "Buzz";

            return "";
        }
    }
}
