using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.ServiceImpl.Base
{
    public class DaoService: WindsorContainer
    {
        private static DaoService _container = null;

        public static readonly DaoService Instance = new DaoService();

        public DaoService()
        {
            _container = new DaoService();

            _container.Install(FromAssembly.This());
        }

    }
}
