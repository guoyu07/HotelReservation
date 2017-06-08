using System;
namespace CustomersRegistry.ServiceComponent
{
    using System.Threading.Tasks;
    using Messages.Commands;
    using NServiceBus;

    class SaveNewReservationCustomerDetailsHandler : IHandleMessages<SaveNewReservationCustomerDetails>
    {
        public Task Handle(SaveNewReservationCustomerDetails message, IMessageHandlerContext context)
        {
            Console.WriteLine("Procesing SaveNewReservationCustomerDetails for reservation: {0} and user: {1} {2}", message.ReservationId, message.CustomerFirstName, message.CustomerLastName);

            return Task.FromResult(0);
        }
    }
}
