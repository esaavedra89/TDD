using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UTest
{
    [TestFixture]
    public class UnitTest3
    {
        [TestCase(1, "I")]
        [TestCase(5, "V")]
        [TestCase(10, "X")]
        [TestCase(2, "II")]
        public void TestMethod1(int expected, string roman)
        {
            Assert.AreEqual(expected, Roman.Parse(roman));
        }
    }

    public class Roman 
    {
        static Dictionary<char, int> map =
            new Dictionary<char, int>() 
            {
                {'I', 1 },
                {'V', 5 },
                {'X', 10 },
                {'L', 50 },
                {'C', 100 },
                {'D', 500 },
                {'M', 1000 }
            };

        public static int Parse(string roman) 
        {
            int result = 0;
            for (int i = 0; i < roman.Length; i++)
            {
                if (i + 1 < roman.Length && IsSubTractive(roman[i], roman[i + 1]))
                {
                    result -= map[roman[i]];
                }
                else
                {
                    result += map[roman[i]];
                }
            }

            return result;

            bool IsSubTractive(char c1, char c2)
            {
                return map[c1] < map[c2];
            }
        }
    }
}
