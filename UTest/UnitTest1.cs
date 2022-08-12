using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TDDPractice.Fibonacci;

namespace UTest
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        public void TestFibonacci(int expected, int index)
        {
            FibonacciClass objFibonacciClass = new FibonacciClass();
            NUnit.Framework.Assert.AreEqual(expected, objFibonacciClass.GetFibonacci(index));
        }
    }
}
