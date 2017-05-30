using System.Web.Http;

namespace Divergent.Sales.API.Host.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        [HttpPost]
        public dynamic Post([FromBody]dynamic customer)
        {
            return 12; //new customer ID
        }
    }
}
