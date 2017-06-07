namespace CustomersRegistry.API.Host.Controllers
{
    using System.Web.Http;

    [RoutePrefix("api/customerswrite")]
    public class CustomersWriteController : ApiController
    {
        [HttpPost]
        public dynamic Post([FromBody]dynamic customer)
        {
            return 12; //new customer ID
        }
    }
}
