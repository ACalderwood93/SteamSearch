using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Fluxter.SteamWebAPI;
using System.Web.Http.Cors;

namespace SteamSearchApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            SteamWebAPI.SetGlobalKey("5F88169B0962B766917B9766FE1B1372");
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
