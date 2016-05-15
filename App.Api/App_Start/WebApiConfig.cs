using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Data.Entity;
using SingleApp.Models;
using System.Web.Http.Cors;
using log4net;
using System.Web.Http.ExceptionHandling;
using System.Text;

namespace SingleApp
{
    public static class WebApiConfig
    {
        public class NLogExceptionLogger : ExceptionLogger
        {
            private static readonly ILog Nlog = log4net.LogManager.GetLogger("myLog");
            public override void Log(ExceptionLoggerContext context)
            {
                

                Nlog.Error(RequestToString(context.Request) + "BALLLLLS" + context.Exception.StackTrace, context.Exception);
            }

            private static string RequestToString(HttpRequestMessage request)
            {
                var message = new StringBuilder();
                if (request.Method != null)
                    message.Append(request.Method);

                if (request.RequestUri != null)
                    message.Append(" ").Append(request.RequestUri);

                return message.ToString();
            }
        }

        public static void Register(HttpConfiguration config)
        {
            config.Services.Add(typeof(IExceptionLogger), new NLogExceptionLogger());
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Context = (System.Runtime.Serialization.StreamingContext)( new SingleAppContext());
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Authentication",
                routeTemplate: "Authentication/Login",
                defaults: new { controller = "Authentication", action = "Login" },
                constraints: null,
                handler:null
            );

            config.Routes.MapHttpRoute(
                name: "Registration",
                routeTemplate: "User/Registration",
                defaults: new { controller = "User", action = "Registration" },
                constraints: null,
                handler: null
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}",
                defaults: new {},
                constraints: null,
                handler: new AuthorizationHandler(config)
                
            );

            
        }
    }
}
