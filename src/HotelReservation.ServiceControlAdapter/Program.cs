using NServiceBus;
using NServiceBus.Configuration.AdvanceExtensibility;
using ServiceControl.TransportAdapter;
using System;
using System.Threading.Tasks;

namespace HotelReservation.ServiceControlAdapter
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            var adapterName = typeof(Program).Namespace;
            Console.Title = adapterName;

            var config = new TransportAdapterConfig<LearningTransport, MsmqTransport>(adapterName);
            config.CustomizeServiceControlTransport(c =>
            {
                c.GetSettings().Set("errorQueue", "error");
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
