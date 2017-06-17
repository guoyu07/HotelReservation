namespace Payments.Service.Component
{
    using NServiceBus;
    using System;
    using System.Threading.Tasks;
    using Reservations.Messages.Events;

    class ReservationCanceledByUserHandler : IHandleMessages<ReservationCanceledByUser>
    {
        public Task Handle(ReservationCanceledByUser message, IMessageHandlerContext context)
        {
            Console.WriteLine("...==============================...\r\n");
            Console.WriteLine(
                "Procesing ReservationCanceledByUser for \r\n ReservationId: {0}", message.ReservationId);

            // TODO: creat a credit payment?

            //using (var db = new PaymentsContext())
            //{
            //    var payment = db.ReservationPayment;

            //    payment.Create<ReservationPaymentDetails>();

            //    payment.Add(new ReservationPaymentDetails
            //    {
            //        PaymentId = Guid.NewGuid(),
            //        ReservationId = Guid.Parse(message.ReservationId),
            //        CustomerId = Guid.Parse(message.CustomerId),
            //        ReservationCancelationDate = message.ReservationCancelationDate,                 
            //    });

            //    await db.SaveChangesAsync();

            //    await context.Publish(new ReservationPaymentComplete
            //    {
            //        ReservationId = message.ReservationId,
            //        PaymentId = message.PaymentId,
            //        PaymentAmount = message.PaymentAmount,
            //    });
            //}

            return Task.CompletedTask;
        }
    }
}
