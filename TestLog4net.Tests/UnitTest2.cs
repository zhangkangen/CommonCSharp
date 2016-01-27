using System;
using NUnit.Framework;
using Platform.Repository;

namespace TestLog4net.Tests
{
    [TestFixture]
    public class UnitTest2
    {
        [SetUp]
        public void init()
        {
            DataManager.InitDataAccess();
        }

        [Test]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void TestMethod2()
        {
            Assert.AreEqual(0.ToString(), "0");
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
