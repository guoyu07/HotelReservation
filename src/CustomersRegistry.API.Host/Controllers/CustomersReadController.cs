namespace CustomersRegistry.API.Host.Controllers
{
    using System;
    using System.Web.Http;

    [RoutePrefix("api/customersread")]
    public class CustomersReadController : ApiController
    {
        [HttpGet]
        public dynamic Get(dynamic customer)
        {
            return new
            {
                CustomerFirstName = "Mauro",
                CustomerLastName = "Servienti",
                CustomerAddress = "v. Antonio Gramsci, 64",
                CustomerCity = "Milano",
                CustomerZipCode = "20100",
                CustomerPhoneNumber = "+39 337 123 098 12",
                CustomerId = Guid.NewGuid(),
            };
        }
    }
}
