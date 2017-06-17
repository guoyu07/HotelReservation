namespace Payments.API.Host.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Data.Context;

    [RoutePrefix("api/reservationsread")]
    public class PaymentsReadController : ApiController
    {
        [HttpGet]
        [Route("~/api/paymentsread/{id}")]
        public dynamic GetById(string id)
        {
            Guid paymentId = Guid.Parse(id);

            using (var db = new PaymentsContext())
            {
                var query = db.ReservationPayment
                    .Where(c => c.PaymentId == paymentId);

                return query.ToArray();
            }
        }
        [HttpGet]
        [Route("~/api/paymentsread/reservation/{id}")]
        public dynamic GetByReservationId(string id)
        {
            Guid reservationId = Guid.Parse(id);

            using (var db = new PaymentsContext())
            {
                var query = db.ReservationPayment
                    .Where(c => c.ReservationId == reservationId);

                return query.ToArray();
            }
        }
    }
}
