namespace Reservations.API.Host.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Http;
    using Data.Context;

    [RoutePrefix("api/reservationsread")]
    public class ReservationsReadController : ApiController
    {
        [HttpGet]
        public dynamic Get(dynamic reservation)
        {
            // get seed data
            Guid reservationId = Guid.Parse("976c44a6-20df-4af7-b1ca-70798ab9df41");

            using (var db = new ResrvationsContext())
            {
                var query = db.ReservationViewModel
                    .Where(c => c.ReservationId == reservationId);

                return query.ToArray();
            }
        }

        [HttpGet]
        [Route("~/api/reservationsread/{id}")]
        public dynamic GetById(string id)
        {
            Guid reservationId = Guid.Parse(id);

            // we might want to poll here if we can't find the reservation in the DB...
            // we could go to a view model :-)

            using (var db = new ResrvationsContext())
            {
                var query = db.ReservationViewModel
                    .Where(c => (c.ReservationId == reservationId && c.UiState== "Completed"));

                if (!query.Any())
                {
                    query = db.ReservationViewModel
                        .Where(c => (c.ReservationId == reservationId && c.UiState == "Pending"));
                }

                return query.ToArray();
            }
        }
    }
}