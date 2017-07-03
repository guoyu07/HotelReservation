namespace CustomersRegistry.API.Host.Controllers
{
    using CustomersRegistry.Messages.Commands;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NServiceBus;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;

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

            // TODO: save customer details to the view model

            await _endpointSession.Send(saveNewReservationCustomerDetails)
                .ConfigureAwait(false);

            return new OkResult(this.Request);
        }
    }
}
