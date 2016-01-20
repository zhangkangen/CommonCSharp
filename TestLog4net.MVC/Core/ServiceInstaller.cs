using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TestLog4net.MVC.Core
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Types.FromAssemblyNamed("Platform.ServiceImpl")
                .Where(type => type.Name.EndsWith("Service"))
                .WithService.DefaultInterfaces()
                );
        }
    }
}