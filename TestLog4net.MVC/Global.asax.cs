using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Platform.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestLog4net.MVC.Plumbing;

namespace TestLog4net.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;

        private static void BootstrapContainer()
        {
            container = new WindsorContainer()
                .Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();

            IConfigurationSource source = System.Configuration.
                ConfigurationManager.GetSection("activerecord")
                as IConfigurationSource;

            ActiveRecordStarter.Initialize(source,
                typeof(Blog));
        }

        protected void Application_End()
        {
            container.Dispose();
        }

    }
}
