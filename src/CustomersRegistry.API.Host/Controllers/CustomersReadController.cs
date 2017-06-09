namespace CustomersRegistry.API.Host.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using CustomerRegistry.Data.Context;

    [RoutePrefix("api/customersread")]
    public class CustomersReadController : ApiController
    {
        [HttpGet]
        public dynamic Get(dynamic customer)
        {
            using (var db = new CustomerRegistryContext())
            {
                var query = db.Customers
                    .Where(c => c.CustomerFirstName == "Mauro");

                return query.ToArray(); // JsonConvert.SerializeObject(query); ;
            }
        }
    }
}
