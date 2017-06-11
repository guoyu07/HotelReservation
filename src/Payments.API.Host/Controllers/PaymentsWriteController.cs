﻿namespace Payments.API.Host.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Messages.Commands;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NServiceBus;

    [RoutePrefix("api/paymentswrite")]
    public class PaymentsWriteController : ApiController
    {
        private readonly IMessageSession _endpointSession;
        public PaymentsWriteController(IMessageSession session)
        {
            _endpointSession = session;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]JToken jsonbody)
        {
            SaveNewPaymentDetails saveNewPaymentDetails = JsonConvert.DeserializeObject<SaveNewPaymentDetails>(jsonbody.ToString());

            await _endpointSession.Send(saveNewPaymentDetails)
                .ConfigureAwait(false);

            return new OkResult(this.Request);
        }
    }
}