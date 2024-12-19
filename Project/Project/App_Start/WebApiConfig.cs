using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace Project
{
    public static class WebApiConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Off;
            routes.EnableFriendlyUrls(settings);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { action = "Index", id = UrlParameter.Optional }
                );  
        }
        //public static void Register(HttpConfiguration config)
        //{
        //    // 啟用 CORS，允許所有來源進行跨域請求
        //    var cors = new EnableCorsAttribute("*", "*", "*");
        //    config.EnableCors(cors);

        //    // 其他的 API 設定
        //    config.MapHttpAttributeRoutes();
        //}
    }
}
