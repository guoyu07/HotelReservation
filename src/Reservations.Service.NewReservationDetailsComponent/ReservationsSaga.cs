namespace Reservations.Service.NewReservationDetailsComponent
{
    using System;
    using System.Threading.Tasks;
    using Messages.Commands;
    using Messages.Events;
    using NServiceBus;
    using NServiceBus.Persistence.Sql;

    /// TODO: make that a saga and add a cancel reservation feature that publishes an event 
    /// and retract the reservation
    /// add a send email function at the customer side?
    public class ReservationsSaga : SqlSaga<ReservationsSagaData>,
        IAmStartedByMessages<SaveNewReservationDetails>,
        IHandleMessages<CancelReservationByUser>,
        IHandleMessages<ReservationPaymentComplete>

    {
        protected override string CorrelationPropertyName => nameof(ReservationsSagaData.ReservationId);

        public Task Handle(SaveNewReservationDetails message, IMessageHandlerContext context)
        {
            Data.ReservationId = message.ReservationId;
            Data.CustomerId = message.CustomerId;
            Data.CheckIn = message.CheckIn;
            Data.CheckOut = message.CheckOut;
            Data.CustomerComments = message.CustomerComments;
            Data.HotelId = message.HotelId;
            Data.PayAtHotel = message.PayAtHotel;


            Console.WriteLine("Procesing SaveNewReservationDetails for \r\n ReservationId: {0} \r\n HotelId: {1} \r\n CheckIn: {2} \r\n CustomerId: {3}", message.ReservationId, message.HotelId, message.CheckIn, message.CustomerId);

            return Task.FromResult(0);
        }

        public Task Handle(CancelReservationByUser message, IMessageHandlerContext context)
        {
            Data.PaymentId = message.ReservationCancelationDate;

            Console.WriteLine("Procesing CancelReservationByUser for \r\n ReservationId: {0}", message.ReservationId);

            return Task.FromResult(0);
        }

        public Task Handle(ReservationPaymentComplete message, IMessageHandlerContext context)
        {
            Data.PaymentId = message.PaymentId;
            Data.PaymentAmount = message.PaymentAmount;

            Console.WriteLine("Procesing ReservationPaymentComplete for \r\n ReservationId: {0} \r\n PaymentId: {1} \r\n PaymentAmount: {2}", message.ReservationId, message.PaymentId, message.PaymentAmount);

            return Task.FromResult(0);
        }

        protected override void ConfigureMapping(IMessagePropertyMapper mapper)
        {
            mapper.ConfigureMapping<SaveNewReservationDetails>(_ => _.ReservationId);
            mapper.ConfigureMapping<CancelReservationByUser>(_ => _.ReservationId);
            mapper.ConfigureMapping<ReservationPaymentComplete>(_ => _.ReservationId);
        }
    }
}