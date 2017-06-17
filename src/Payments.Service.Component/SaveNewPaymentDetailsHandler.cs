namespace Payments.Service.Component
{
    using NServiceBus;
    using Messages.Commands;
    using System;
    using System.Threading.Tasks;
    using Data.Context;
    using Data.Models;
    using Reservations.Messages.Events;

    class SaveNewPaymentDetailsHandler : IHandleMessages<SaveNewPaymentDetails>
    {
        public async Task Handle(SaveNewPaymentDetails message, IMessageHandlerContext context)
        {
            Console.WriteLine("...==============================...\r\n");
            Console.WriteLine(
                "Procesing SaveNewPaymentDetails for \r\n ReservationId: {0} \r\n Amount: {1} \r\n Card Number: {2}",
                message.ReservationId, message.PaymentAmount, message.CardNumber);

            using (var db = new PaymentsContext())
            {
                var payment = db.ReservationPayment;

                payment.Create<ReservationPaymentDetails>();
                payment.Add(new ReservationPaymentDetails
                {
                    PaymentId = Guid.Parse(message.PaymentId),
                    ReservationId = Guid.Parse(message.ReservationId),
                    CustomerId = Guid.Parse(message.CustomerId),
                    PaymentAmount = message.PaymentAmount,
                    CardNumber = message.CardNumber,
                    CCV = message.CCV,
                    ExpieryDateMonth = message.ExpieryDateMonth,
                    ExpieryDateYear = message.ExpieryDateYear,
                    CardType = message.CardType,
                });

                await db.SaveChangesAsync();

                await context.Publish(new ReservationPaymentComplete
                {
                    ReservationId = message.ReservationId,
                    PaymentId = message.PaymentId,
                    PaymentAmount = message.PaymentAmount,
                });
            }
        }
    }
}
