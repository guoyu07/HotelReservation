namespace Reservations.Messages.Events
{
    public class ReservationCanceledByUser
    {
        public string ReservationId { get; set; }
        public string CustomerId { get; set; }
        public string ReservationCancelationDate { get; set; }
    }
}
