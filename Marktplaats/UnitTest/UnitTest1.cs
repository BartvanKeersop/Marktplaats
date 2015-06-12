using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Som s = new Som();
            Assers.AreEqual((s.Optellen(1, 2)), 3);
        }
    }
}
