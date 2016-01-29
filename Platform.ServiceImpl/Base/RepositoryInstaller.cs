using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Platform.IRepository;
using Platform.Repository;

namespace Platform.ServiceImpl.Base
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                //Types.FromAssemblyNamed("Platform.Repository")
                //.Where(type => type.Name.EndsWith("Repository"))
                //.WithService.DefaultInterfaces(),
                Component.For<IModuleRepository>().ImplementedBy<ModuleRepository>()
                );
        }
    }
}
