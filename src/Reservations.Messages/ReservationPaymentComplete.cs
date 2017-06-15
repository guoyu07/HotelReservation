namespace Reservations.Messages.Events
{
    public class ReservationPaymentComplete
    {
        public string ReservationId { get; set; }
        public string PaymentId { get; set; }
        public string PaymentAmount { get; set; }
    }
}
