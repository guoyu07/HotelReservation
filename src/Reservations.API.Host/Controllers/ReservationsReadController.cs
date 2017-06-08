namespace Reservations.API.Host.Controllers
{
    using System.Web.Http;

    [RoutePrefix("api/reservationsread")]
    public class ReservationsReadController : ApiController
    {
        [HttpGet]
        public dynamic Get(dynamic reservation)
        {
            return new
            {
                ReservationId = "",
                CustomerId = "",
                HotelId = "",
                CheckIn = "15/07/2017",
                CheckOut = "17/07/2017",
                PayAtHotel = true,
                PaymentId = "",
                CustomerComments = "",
                HotelName = "Sample Hotel",
            };
        }
    }
}