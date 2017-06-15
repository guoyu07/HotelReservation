namespace CustomersRegistry.ViewModelComposition
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using ITOps.ViewModelComposition;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Model;
    using Newtonsoft.Json;

    class NewReservationCustomerGetHandler : IHandleRequests
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

        public async Task HandleAsync(dynamic vm, RouteData routeData, HttpRequest request)
        {
            CustomersRegistryDetailsModel customerDetails;

            if (request.HttpContext.User.Identity.IsAuthenticated)
            {
                /*
                 * try read customer info from backend API 
                 * based on current user id and pre-fill 
                 * the submission form
                 */
                customerDetails = await GetAuthenticatedUserDetailsAsync();
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
                customerDetails = await GetAuthenticatedUserDetailsAsync();
                MapCustomerDetailsToDynamic(vm, customerDetails);
            }
        }

        private void MapCustomerDetailsToDynamic(dynamic vm, CustomersRegistryDetailsModel customerDetails)
        {
            vm.CustomerFirstName = customerDetails.CustomerFirstName; // "Mauro";
            vm.CustomerLastName = customerDetails.CustomerLastName; // "Servienti";
            vm.CustomerAddress = customerDetails.CustomerAddress; // "v. Antonio Gramsci, 64";
            vm.CustomerCity = customerDetails.CustomerCity; // "Milano";
            vm.CustomerZipCode = customerDetails.CustomerZipCode; // "20100";
            vm.CustomerPhoneNumber = customerDetails.CustomerPhoneNumber; // "+39 337 123 098 12";
            vm.CustomerId = customerDetails.CustomerId;
        }

        private async Task<CustomersRegistryDetailsModel> GetAuthenticatedUserDetailsAsync()
        {
            var result = await CustomerReadAPIAsync();
            return JsonConvert.DeserializeObject<IList<CustomersRegistryDetailsModel>>(await result.Content.ReadAsStringAsync())[0];
        }

        private async Task<HttpResponseMessage> CustomerReadAPIAsync()
        {
            const string uri = "http://localhost:8181";
            const string url = "/api/customersread";

            HttpClient httpClient = new HttpClient {BaseAddress = new Uri(uri)};

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await httpClient.GetAsync(url);
        }
    }
}