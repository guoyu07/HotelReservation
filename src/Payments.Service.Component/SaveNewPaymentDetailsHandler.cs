namespace Payments.Service.Component
{
    using NServiceBus;
    using Messages.Commands;
    using System;
    using System.Threading.Tasks;
    using Reservations.Messages.Events;

    class SaveNewPaymentDetailsHandler : IHandleMessages<SaveNewPaymentDetails>
    {
        public async Task Handle(SaveNewPaymentDetails message, IMessageHandlerContext context)
        {
            Console.WriteLine("Procesing SaveNewPaymentDetails for \r\n ReservationId: {0} \r\n Amount: {1} \r\n Card Number: {2}", message.ReservationId, message.PaymentAmount, message.CardNumber);

            await context.Publish(new ReservationPaymentComplete
            {
                ReservationId = message.ReservationId,
                PaymentId = message.PaymentId,
                PaymentAmount = message.PaymentAmount,
            });
        }
    }
}
