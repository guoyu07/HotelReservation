using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using NServiceBus;
using Owin;
using Radical.Bootstrapper;
using Radical.Bootstrapper.Windsor.WebAPI.Infrastructure;
using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Batch;

namespace Reservations.API.Host
{
    using Messages.Commands;
    using NServiceBus.Features;

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            Console.Title = typeof(Startup).Namespace;

            var bootstrapper = new WindsorBootstrapper(AppDomain.CurrentDomain.BaseDirectory, filter: "Reservations*.*");
            var container = bootstrapper.Boot();

            ConfigureNServiceBus(container);
            ConfigureWebAPI(appBuilder, container);
        }

        void ConfigureWebAPI(IAppBuilder appBuilder, IWindsorContainer container)
        {
            var config = new HttpConfiguration();
            var server = new HttpServer(config);

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.DependencyResolver = new WindsorDependencyResolver(container);

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

        void ConfigureNServiceBus(IWindsorContainer container)
        {
            var endpointConfiguration = new EndpointConfiguration(typeof(Startup).Namespace);
            endpointConfiguration.UseSerialization<NServiceBus.JsonSerializer>();
            endpointConfiguration.UseContainer<WindsorBuilder>(c => c.ExistingContainer(container));

            var transportExtensions = endpointConfiguration.UseTransport<MsmqTransport>();

            endpointConfiguration.HeartbeatPlugin(
                serviceControlQueue: "particular.servicecontrol",
                frequency: TimeSpan.FromSeconds(30),
                timeToLive: TimeSpan.FromMinutes(3));

            var routing = transportExtensions.Routing();
            routing.RouteToEndpoint(
                messageType: typeof(SaveNewReservationDetails),
                destination: "Reservations.Service.NewReservationDetailsComponent");

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.DisableFeature<TimeoutManager>();
            endpointConfiguration.DisableFeature<MessageDrivenSubscriptions>();

            var endpointInstance = Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            container.Register(Component.For<IMessageSession>().Instance(endpointInstance));
        }
    }
}