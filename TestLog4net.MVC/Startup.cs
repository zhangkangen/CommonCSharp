using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestLog4net.MVC.Startup))]
namespace TestLog4net.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
