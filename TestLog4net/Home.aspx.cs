using log4net;
using Log4net.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestLog4net
{
    public partial class Home : System.Web.UI.Page
    {
        private static readonly LogWrapper _logger = new LogWrapper();

        protected void Page_Load(object sender, EventArgs e)
        {
            _logger.Debug("home页面2");
        }

        public void test()
        {
            var a = 0;
            var i = 1 / a;
        }
    }
}