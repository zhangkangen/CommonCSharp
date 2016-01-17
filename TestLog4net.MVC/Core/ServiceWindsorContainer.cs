using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestLog4net.MVC.Core
{
    public class ServiceWindsorContainer : Castle.Windsor.WindsorContainer
    {
        private readonly ServiceWindsorContainer _serviceWindsorContainer;

        public static readonly ServiceWindsorContainer instance = new ServiceWindsorContainer();

        public ServiceWindsorContainer()
        {
            _serviceWindsorContainer = new ServiceWindsorContainer();
            _serviceWindsorContainer.Install(FromAssembly.This());
        }
    }
}