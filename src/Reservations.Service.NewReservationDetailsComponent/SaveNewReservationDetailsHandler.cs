namespace Reservations.Service.NewReservationDetailsComponent
{
    using System;
    using System.Threading.Tasks;
    using Messages.Commands;
    using NServiceBus;

    class SaveNewReservationDetailsHandler : IHandleMessages<SaveNewReservationDetails>
    {
        public Task Handle(SaveNewReservationDetails message, IMessageHandlerContext context)
        {
            Console.WriteLine("Procesing SaveNewReservationDetails for \r\n ReservationId: {0} \r\n HotelId: {1} \r\n CheckIn: {2} \r\n CustomerId: {3}", message.ReservationId, message.HotelId, message.CheckIn, message.CustomerId);

            return Task.FromResult(0);
        }
    }
}
