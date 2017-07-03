namespace Reservations.ViewModelComposition.Model
{
    internal class ReservationDetailsModel
    {
        public string ReservationId { get; set; }
        public string CustomerId { get; set; }
        public string HotelId { get; set; }
        public string HotelName { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string PayAtHotel { get; set; }
        public string PayNow { get; set; }
        public string CustomerComments { get; set; }
        public string PaymentId { get; set; }
        public string UiState { get; set; }
    }
}