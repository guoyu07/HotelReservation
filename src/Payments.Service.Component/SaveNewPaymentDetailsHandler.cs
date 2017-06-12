namespace Reservations.Service.NewReservationDetailsComponent
{
    using System;
    using System.Threading.Tasks;
    using Messages.Commands;
    using NServiceBus;

    class SaveNewPaymentDetailsHandler : IHandleMessages<SaveNewPaymentDetails>
    {
        public Task Handle(SaveNewPaymentDetails message, IMessageHandlerContext context)
        {
            Console.WriteLine("Procesing SaveNewPaymentDetails for \r\n ReservationId: {0} \r\n Amount: {1} \r\n Card Number: {2}", message.ReservationId, message.PaymentAmount, message.CardNumber);

            return Task.FromResult(0);
        }
    }
}
