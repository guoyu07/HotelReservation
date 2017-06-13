namespace Payments.Service.Component
{
    using NServiceBus;
    using Reservations.Messages.Commands;
    using System;
    using System.Threading.Tasks;

    class SaveNewPaymentDetailsHandler : IHandleMessages<SaveNewPaymentDetails>
    {
        public Task Handle(SaveNewPaymentDetails message, IMessageHandlerContext context)
        {
            Console.WriteLine("Procesing SaveNewPaymentDetails for \r\n ReservationId: {0} \r\n Amount: {1} \r\n Card Number: {2}", message.ReservationId, message.PaymentAmount, message.CardNumber);

            return Task.FromResult(0);
        }
    }
}
