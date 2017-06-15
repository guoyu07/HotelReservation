namespace CustomersRegistry.Service.Component
{
    using NServiceBus;
    using Reservations.Messages.Events;
    using System;
    using System.Threading.Tasks;

    class NewReservationCompletedHandler : IHandleMessages<NewReservationCompleted>
    {
        public Task Handle(NewReservationCompleted message, IMessageHandlerContext context)
        {
            Console.WriteLine("...==============================...\r\n");
            Console.WriteLine("Procesing NewReservationCompleted for \r\n ReservationId: {0},  \r\n Sending a confermtion email to the user...", message.ReservationId );

           // sendf a welcome email

            return Task.FromResult(0);
        }
    }
}
