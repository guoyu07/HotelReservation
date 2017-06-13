namespace Reservations.Messages.Events
{
    public class NewReservationCompleted
    {
        public string ReservationId { get; set; }
        public string CustomerId { get; set; }
    }
}
