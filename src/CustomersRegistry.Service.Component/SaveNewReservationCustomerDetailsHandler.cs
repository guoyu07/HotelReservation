using System;
namespace CustomersRegistry.Service.Component
{
    using NServiceBus;
    using CustomersRegistry.Messages.Commands;
    using System.Threading.Tasks;

    class SaveNewReservationCustomerDetailsHandler : IHandleMessages<SaveNewReservationCustomerDetails>
    {
        public Task Handle(SaveNewReservationCustomerDetails message, IMessageHandlerContext context)
        {
            Console.WriteLine("Procesing SaveNewReservationCustomerDetails for \r\n ReservationId: {0} \r\n User: {1} {2} \r\n CustomerId: {3}", message.ReservationId, message.CustomerFirstName, message.CustomerLastName, message.CustomerId);

            //save details to db

            return Task.FromResult(0);
        }
    }
}
