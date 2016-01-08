using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestLog4net.MVC.Controllers
{
    public class BaseController : Controller
    {
        public int CurrentAppId
        {
            get { return 410; }
        }
    }
}