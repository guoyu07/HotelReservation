using System;
using System.Threading.Tasks;

namespace Reservations.Service.NewReservationDetailsComponent
{
    using System.Data.SqlClient;
    using Messages.Events;
    using NServiceBus;
    using NServiceBus.Persistence.Sql;

    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "Reservations.Service.NewReservationDetailsComponent";

            var endpointConfiguration = new EndpointConfiguration("Reservations.Service.NewReservationDetailsComponent");
            endpointConfiguration.UseSerialization<JsonSerializer>();
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));

            var connection = @"Data Source=.\SQLEXPRESS;Initial Catalog=HotelReservationsPersistence;Integrated Security=True";
            persistence.SqlVariant(SqlVariant.MsSqlServer);
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(connection);
                });
            
            var transportExtensions = endpointConfiguration.UseTransport<MsmqTransport>();
            var routing = transportExtensions.Routing();
            routing.RegisterPublisher(
                eventType: typeof(ReservationPaymentComplete),
                publisherEndpoint: "Payments.Service.Component");
            endpointConfiguration.HeartbeatPlugin(
                serviceControlQueue: "particular.servicecontrol",
                frequency: TimeSpan.FromSeconds(30),
                timeToLive: TimeSpan.FromMinutes(3));
            endpointConfiguration.SagaPlugin(
                serviceControlQueue: "particular.servicecontrol");
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            try
            {
                Console.WriteLine("\r\nBus created and configured; press any key to stop program\r\n");
                Console.ReadKey();
            }
            finally
            {
                await endpointInstance.Stop()
                    .ConfigureAwait(false);
            }
        }
    }
}
