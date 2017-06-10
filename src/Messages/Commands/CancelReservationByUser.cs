namespace Messages.Commands
{
    public class CancelReservationByUser
    {
        public string ReservationId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerComments { get; set; }
        public string ReservationCancelationDate { get; set; }
    }
}
