namespace CustomersRegistry.API.Host.Controllers
{
    using System.Threading.Tasks;
    using NServiceBus;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Messages.Commands;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [RoutePrefix("api/customerswrite")]
    public class CustomersWriteController : ApiController
    {
        private readonly IMessageSession _endpointSession;
        public CustomersWriteController(IMessageSession session)
        {
            _endpointSession = session;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]JToken jsonbody)
        {
            SaveNewReservationCustomerDetails saveNewReservationCustomerDetails = JsonConvert.DeserializeObject<SaveNewReservationCustomerDetails>(jsonbody.ToString());

            await _endpointSession.Send(saveNewReservationCustomerDetails)
                .ConfigureAwait(false);

            return new OkResult(this.Request);
        }
    }
}
