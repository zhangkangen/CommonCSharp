using Microsoft.VisualStudio.TestTools.UnitTesting;
using Platform.Model;
using Platform.Repository;
using System.Collections.Generic;

namespace TestLog4net.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testcontext)
        {
            DataManager.InitDataAccess();
        }

        [TestMethod]
        public void TestMethod1()
        {
            //var blogs = Blog.FindAll();

            //Assert.IsNull(blogs);
        }
    }
}
