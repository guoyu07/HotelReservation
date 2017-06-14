using System;
using System.Threading.Tasks;

namespace Reservations.Service.NewReservationDetailsComponent
{
    using NServiceBus;

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
            endpointConfiguration.UsePersistence<LearningPersistence>();            
            endpointConfiguration.UseTransport<LearningTransport>();

            endpointConfiguration.HeartbeatPlugin(
                serviceControlQueue: "particular.servicecontrol",
                frequency: TimeSpan.FromSeconds(30),
                timeToLive: TimeSpan.FromMinutes(3));
            endpointConfiguration.SagaPlugin(
                serviceControlQueue: "particular.servicecontrol");
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

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
