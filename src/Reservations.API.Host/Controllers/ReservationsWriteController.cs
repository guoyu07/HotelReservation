namespace Reservations.API.Host.Controllers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NServiceBus;
    using Messages.Commands;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;

    [RoutePrefix("api/reservationswrite")]
    public class ReservationsWriteController : ApiController
    {
        private readonly IMessageSession _endpointSession;
        public ReservationsWriteController(IMessageSession session)
        {
            _endpointSession = session;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]JToken jsonbody)
        {
            SaveNewReservationDetails saveNewReservationDetails = JsonConvert.DeserializeObject<SaveNewReservationDetails>(jsonbody.ToString());

            await _endpointSession.Send(saveNewReservationDetails)
                .ConfigureAwait(false);

            return new OkResult(this.Request);
        }

        [HttpPost]
        [Route("~/api/reservationswrite/{id}/cancel")]
        public async Task<IHttpActionResult> CancelReservation([FromBody]JToken jsonbody, string id)
        {
        CancelReservationByUser cancelReservationByUser = JsonConvert.DeserializeObject<CancelReservationByUser>(jsonbody.ToString());

        await _endpointSession.Send(cancelReservationByUser)
            .ConfigureAwait(false);

            return new OkResult(this.Request);
        }
    }
}