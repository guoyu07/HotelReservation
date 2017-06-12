using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Batch;

namespace CustomersRegistry.API.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            var server = new HttpServer(config);

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            //config.DependencyResolver = new WindsorDependencyResolver(container);

            config.Formatters
                .JsonFormatter
                .SerializerSettings
                .ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpBatchRoute(
                routeName: "batch",
                routeTemplate: "api/batch",
                batchHandler: new DefaultHttpBatchHandler(server)
            );

            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseWebApi(config);
        }
    }
}