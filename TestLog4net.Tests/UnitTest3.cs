using System;
using Platform.IRepository;
using Platform.ServiceImpl.Base;
using Platform.Repository;
using NUnit.Framework;
using Platform.ServiceInterface;

namespace TestLog4net.Tests
{
    [TestFixture]
    public class UnitTest3
    {
        [Test]
        public void TestMethod1()
        {
            IModuleRepository moduleRepository = RepositoryContainer.instance.Resolve<IModuleRepository>();
            IModuleService moduleService = MVC.Core.ServiceWindsorContainer.instance.Resolve<IModuleService>();
            Assert.IsNotNull(moduleRepository);
            Assert.IsNotNull(moduleService);
            moduleService.Get();
        }
    }
}
