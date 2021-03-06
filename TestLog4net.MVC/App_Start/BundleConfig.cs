﻿using System.Web;
using System.Web.Optimization;

namespace TestLog4net.MVC
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            //bootstrap js
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            //easyui js
            bundles.Add(new ScriptBundle("~/bundles/easyui").Include(
                "~/Scripts/jquery.easyui-{version}.js"));

            //easyloader.js
            bundles.Add(new ScriptBundle("~/bundles/easyloader").Include(
                "~/Scripts/easyloader.js"));

            //easyui css
            bundles.Add(new StyleBundle("~/Content/easyui").Include(
                "~/Content/themes/bootstrap/easyui.css",
                "~/Content/themes/icon.css",
                "~/Content/themes/color.css",
                "~/Content/themes/mobile.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css"));
        }
    }
}
