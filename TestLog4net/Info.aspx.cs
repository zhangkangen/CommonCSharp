using log4net;
using Log4net.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestLog4net
{
    public partial class Info : System.Web.UI.Page
    {
        private static readonly LogWrapper _logger = new LogWrapper();
        protected void Page_Load(object sender, EventArgs e)
        {
            _logger.Debug("info页面");
        }
    }
}