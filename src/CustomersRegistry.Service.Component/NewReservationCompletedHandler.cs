namespace CustomersRegistry.ServiceComponent
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus;
    using Messages.Events;

    class NewReservationCompletedHandler : IHandleMessages<NewReservationCompleted>
    {
        public Task Handle(NewReservationCompleted message, IMessageHandlerContext context)
        {
            Console.WriteLine("Procesing NewReservationCompleted for \r\n ReservationId: {0},  \r\n Sending a confermtion email to the user...", message.ReservationId );

            return Task.FromResult(0);
        }
    }
}
