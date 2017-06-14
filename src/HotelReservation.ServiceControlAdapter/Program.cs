using NServiceBus;
using ServiceControl.TransportAdapter;
using System;
using System.Threading.Tasks;

namespace HotelReservation.ServiceControlAdapter
{
    using NServiceBus.Configuration.AdvanceExtensibility;

    class Program
    {
        const string adapterName = "HotelReservation.ServiceControlAdapter";

        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = adapterName;

            var config = new TransportAdapterConfig<LearningTransport, MsmqTransport>(adapterName);

            config.CustomizeServiceControlTransport(
                customization: transport =>
                {
                    //HACK: Latest MSMQ requires this setting. To be moved to the transport adapter core.
                    transport.GetSettings().Set("errorQueue", "poison");
                });

            var adapter = TransportAdapter.Create(config);
            await adapter.Start().ConfigureAwait(false);

            Console.WriteLine();
            Console.WriteLine($"{adapterName} created and configured; press any key to stop program.");
            Console.WriteLine();
            Console.ReadKey();

            await adapter.Stop().ConfigureAwait(false);
        }
    }
}
