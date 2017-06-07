using ITOps.ViewModelComposition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CustomersRegistry.ViewModelComposition
{
    class NewReservationCustomerDetailsHandler : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            //this is the reservation summary before final confirmation by the user
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsGet(httpVerb)
                && controller.ToLowerInvariant() == "reservation"
                && action.ToLowerInvariant() == "new";
        }

        public Task HandleAsync(dynamic vm, RouteData routeData, HttpRequest request)
        {
            CustomersRegistryDetails customerDetails;

            if (request.HttpContext.User.Identity.IsAuthenticated)
            {
                /*
                 * try read customer info from backend API 
                 * based on current user id and pre-fill 
                 * the submission form
                 */
                customerDetails = GetAuthenticatedUserDetailsAsync();

                MapCustomerDetailsToDynamic(vm, customerDetails);
            }
            else
            {
                /*
                 * add to vm an empty customer model 
                 * ready to be filled in the form
                 * 
                 * for now we will get teh data from the read API
                 */
                customerDetails = GetAuthenticatedUserDetailsAsync();

                MapCustomerDetailsToDynamic(vm, customerDetails);
            }

            return Task.CompletedTask;
        }

        private static void MapCustomerDetailsToDynamic(dynamic vm, CustomersRegistryDetails customerDetails)
        {
            vm.CustomerFirstName = customerDetails.CustomerFirstName; // "Mauro";
            vm.CustomerLastName = customerDetails.CustomerLastName; // "Servienti";
            vm.CustomerAddress = customerDetails.CustomerAddress; // "v. Antonio Gramsci, 64";
            vm.CustomerCity = customerDetails.CustomerCity; // "Milano";
            vm.CustomerZipCode = customerDetails.CustomerZipCode; // "20100";
            vm.CustomerPhoneNumber = customerDetails.CustomerPhoneNumber; // "+39 337 123 098 12";
        }

        private CustomersRegistryDetails GetAuthenticatedUserDetailsAsync()
        {
            HttpResponseMessage result = null;

            // call the read API
            Task.Run(() => { result = GetWebPageHtmlSizeAsync().Result; }).Wait();

            return JsonConvert.DeserializeObject<CustomersRegistryDetails>(result.Content.ReadAsStringAsync().Result);
        }

        private async Task<HttpResponseMessage> GetWebPageHtmlSizeAsync()
        {
            const string uri = "http://localhost:8181";
            const string url = "/api/customersread";

            HttpClient httpClient = new HttpClient {BaseAddress = new Uri(uri)};

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await httpClient.GetAsync(url);
        }
    }

    internal class CustomersRegistryDetails
    {
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerZipCode { get; set; }
        public string CustomerPhoneNumber { get; set; }
    }
}
