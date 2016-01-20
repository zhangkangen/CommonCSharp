using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestLog4net.MVC.Core
{
    public class ServiceWindsorContainer : Castle.Windsor.WindsorContainer
    {
        public static readonly ServiceWindsorContainer instance = new ServiceWindsorContainer();

        public ServiceWindsorContainer()
        {
            Install(new ServiceInstaller());
        }
    }
}