namespace Reservations.Service.NewReservationDetailsComponent
{
    using NServiceBus;
    using Messages.Commands;
    using Messages.Events;
    using System;
    using System.Threading.Tasks;

    /// TODO: make that a saga and add a cancel reservation feature that publishes an event 
    /// and retract the reservation
    /// add a send email function at the customer side?
    public class ReservationsSaga : Saga<ReservationsSagaData>,
        IAmStartedByMessages<SaveNewReservationDetails>,
        IHandleMessages<CancelReservationByUser>,
        IAmStartedByMessages<ReservationPaymentComplete>

    {
        public Task Handle(SaveNewReservationDetails message, IMessageHandlerContext context)
        {
            Data.ReservationId = message.ReservationId;
            Data.CustomerId = message.CustomerId;
            Data.CheckIn = message.CheckIn;
            Data.CheckOut = message.CheckOut;
            Data.CustomerComments = message.CustomerComments;
            Data.HotelId = message.HotelId;
            Data.PayAtHotel = message.PayAtHotel;
            Data.ReservationSaved = true;

            Console.WriteLine("...==============================...\r\n");
            Console.WriteLine("Procesing SaveNewReservationDetails for \r\n ReservationId: {0} \r\n HotelId: {1} \r\n CheckIn: {2} \r\n CustomerId: {3}", message.ReservationId, message.HotelId, message.CheckIn, message.CustomerId);

            CheckIfSagaIsComplete(context);

            return Task.FromResult(0);
        }

        private void CheckIfSagaIsComplete(IMessageHandlerContext context)
        {
            if (Data.PaymentComplete && Data.ReservationSaved)
            {
                Data.SagaCompleted = true;

                Console.WriteLine("Saga Completed for \r\n ReservationId: {0}", Data.ReservationId);

                context.Publish<NewReservationCompleted>(
                    m =>
                    {
                        m.ReservationId = Data.ReservationId;
                        m.CustomerId = Data.CustomerId;
                    });
            }
        }

        public Task Handle(CancelReservationByUser message, IMessageHandlerContext context)
        {
            Data.PaymentId = message.ReservationCancelationDate;

            Console.WriteLine("...==============================...\r\n");
            Console.WriteLine("Procesing CancelReservationByUser for \r\n ReservationId: {0}", message.ReservationId);

            // kick off a cancelation process 

            return Task.FromResult(0);
        }

        public Task Handle(ReservationPaymentComplete message, IMessageHandlerContext context)
        {
            Data.PaymentId = message.PaymentId;
            Data.PaymentAmount = message.PaymentAmount;
            Data.PaymentComplete = true;

            Console.WriteLine("...==============================...\r\n");
            Console.WriteLine("Procesing ReservationPaymentComplete for \r\n ReservationId: {0} \r\n PaymentId: {1} \r\n PaymentAmount: {2}", message.ReservationId, message.PaymentId, message.PaymentAmount);

            CheckIfSagaIsComplete(context);

            return Task.FromResult(0);
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ReservationsSagaData> mapper)
        {
            mapper.ConfigureMapping< SaveNewReservationDetails>(m => m.ReservationId).ToSaga(s => s.ReservationId);
            mapper.ConfigureMapping<CancelReservationByUser>(m => m.ReservationId).ToSaga(s => s.ReservationId);
            mapper.ConfigureMapping<ReservationPaymentComplete>(m => m.ReservationId).ToSaga(s => s.ReservationId);
        }
    }
}