using Platform.IRepository;
using Platform.ServiceImpl.Base;
using Platform.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ServiceImpl.Common.Permission
{

    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository moduleRepository;

        public ModuleService()
        {
            moduleRepository = RepositoryContainer.instance.Resolve<IModuleRepository>();
        }

        public int Get()
        {
            return moduleRepository.Get();
        }
    }
}
