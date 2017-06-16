namespace CustomersRegistry.ViewModelComposition
{
    using System;
    using System.Net;
    using ITOps.ViewModelComposition;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Model;
    using Newtonsoft.Json;

    class NewReservationCustomerPostHandler : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            //this is the POST interceptor when reservation is submitted
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsPost(httpVerb)
                && controller.ToLowerInvariant() == "reservation"
                && action.ToLowerInvariant() == "new";
        }

        public Task HandleAsync(dynamic vm, RouteData routeData, HttpRequest request)
        {
            var form = request.Form;
            
            /*  
             * Get Customer details from incoming FORM
             * and post them to Customers Registry API
             */
            var customerDetails = MapFormToCustomerDetails(form);

            PostCustomerDetails(customerDetails).Wait();

            return Task.CompletedTask;
        }

        private CustomersRegistryDetailsModel MapFormToCustomerDetails(IFormCollection form)
        {
            return new CustomersRegistryDetailsModel
            {
                CustomerFirstName = form["CustomerFirstName"],
                CustomerLastName = form["CustomerLastName"],
                CustomerAddress = form["CustomerAddress"],
                CustomerCity = form["CustomerCity"],
                CustomerZipCode = form["CustomerZipCode"],
                CustomerPhoneNumber = form["CustomerPhoneNumber"],
                ReservationId = form["ReservationId"],
                CustomerId = form["CustomerId"],
            };
        }

        private async Task PostCustomerDetails(CustomersRegistryDetailsModel customerDetails)
        {
            HttpContent jasonHttpContent = new StringContent(JsonConvert.SerializeObject(customerDetails),
                Encoding.UTF8, "application/json");

            var result = await CustomerWriteApiTask(jasonHttpContent);

            HttpStatusCode resultStatusCode = result.StatusCode;

            if (resultStatusCode == HttpStatusCode.OK)
            {
            }

            if (resultStatusCode != HttpStatusCode.OK)
            {
                // issues? 
            }
        }

        private async Task<HttpResponseMessage> CustomerWriteApiTask(HttpContent content)
        {
            const string uri = "http://localhost:8181";
            const string url = "/api/customerswrite/cancel";

            HttpClient httpClient = new HttpClient {BaseAddress = new Uri(uri)};

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await httpClient.PostAsync(url, content);
        }
    }
}
