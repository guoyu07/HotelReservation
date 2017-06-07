namespace CustomersRegistry.API.Host.Controllers
{
    using NServiceBus;
    using System.Web.Http;

    [RoutePrefix("api/customerswrite")]
    public class CustomersWriteController : ApiController
    {
        public CustomersWriteController(IMessageSession session)
        {

        }

        [HttpPost]
        public dynamic Post([FromBody]dynamic customer)
        {
            return 12; //new customer ID
        }
    }
}
