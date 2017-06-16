namespace Reservations.API.Host.Controllers
{
    using System;
    using System.Web.Http;

    [RoutePrefix("api/reservationsread")]
    public class ReservationsReadController : ApiController
    {
        [HttpGet]
        public dynamic Get(dynamic reservation)
        {
            return new
            {
                HotelId = Guid.NewGuid(),
                CheckIn = "15/07/2017",
                CheckOut = "17/07/2017",
                PayAtHotel = true,
                CustomerComments = "",
                HotelName = "Sample Hotel",
            };
        }

        [HttpGet]
        [Route("~/api/reservationsread/{id}")]
        public dynamic GetById(string id)
        {
            return new
            {
                HotelId = Guid.NewGuid(),
                CheckIn = "15/07/2017",
                CheckOut = "17/07/2017",
                PayAtHotel = true,
                CustomerComments = "",
                HotelName = "Sample Hotel",
                ReservationId = id,
            };
        }
    }
}