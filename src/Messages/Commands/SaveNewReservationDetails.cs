namespace Messages.Commands
{
    public class SaveNewReservationDetails
    {
        public string ReservationId { get; set; }
        public string CustomerId { get; set; }
        public string HotelId { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string PayAtHotel { get; set; }
        public string CustomerComments { get; set; }
        public string ReservationDate { get; set; }
    }
}
