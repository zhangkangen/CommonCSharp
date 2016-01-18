using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestLog4net.MVC.Core;

namespace TestLog4net.MVC.Controllers
{
    public class HomeController : BaseController
    {
        #region Service
        
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region 菜单
        public JsonResult GetMenu()
        {


            return new JsonResult
            {
            };
        }
        #endregion
    }
}