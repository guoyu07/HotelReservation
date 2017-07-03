namespace CustomersRegistry.API.Host.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Messages.Commands;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NServiceBus;

    [RoutePrefix("api/customerswrite/cancel")]
    public class CustomersCancelWriteController : ApiController
    {
        private readonly IMessageSession _endpointSession;
        public CustomersCancelWriteController(IMessageSession session)
        {
            _endpointSession = session;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]JToken jsonbody)
        {
            CancelReservationCustomerDetails cancelReservationCustomerDetails = JsonConvert.DeserializeObject<CancelReservationCustomerDetails>(jsonbody.ToString());

            // add/update the customer to the viewModel as pending

            await _endpointSession.Send(cancelReservationCustomerDetails)
                .ConfigureAwait(false);

            return new OkResult(this.Request);
        }
    }
}