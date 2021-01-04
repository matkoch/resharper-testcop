﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCop.TestApplication2;

namespace TestCop.TestApplication2Tests
{
    [TestClass]
    public class ClassATests
    {
        /* test to confirm that we can have a custom testing namespace suffix */
        [TestMethod]
        public void ReturnsTrueMethodTest()
        {
            Assert.IsTrue(new ClassA().ReturnsTrue());
        }
    }
}
