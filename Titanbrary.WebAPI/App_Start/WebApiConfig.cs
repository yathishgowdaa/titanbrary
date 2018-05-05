using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace Titanbrary.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));


            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            //WebApi: This is what transforms all the Json into camelcase for us.
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            ////WebApi: Accept multipartFormData
            json.SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));

            json.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            var cors = new EnableCorsAttribute("*", "*", "*");
            //support CORS
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //MediaTypeHeaderValue appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            
        }
    }
}
