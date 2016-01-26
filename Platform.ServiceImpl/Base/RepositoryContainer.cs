using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ServiceImpl.Base
{
    public class RepositoryContainer: WindsorContainer
    {
        public static readonly RepositoryContainer instance = new RepositoryContainer();

        public RepositoryContainer()
        {
            Install(new RepositoryInstaller());
        }
    }
}
