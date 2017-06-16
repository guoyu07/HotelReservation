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

    class ReservationCustomerCancelConfirmPostHandler : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            //this is the reservation details after a reservation has been submitted
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsPost(httpVerb)
                && controller.ToLowerInvariant() == "reservation"
                && action.ToLowerInvariant() == "cancelconfirm";
        }

        public async Task HandleAsync(dynamic vm, RouteData routeData, HttpRequest request)
        {
            var reservationId = request.Query["rid"];
            CustomersRegistryDetailsModel customerDetails;

            /*
             * Get reservation details from WebAPI
             */
            customerDetails = await GetAuthenticatedUserDetailsAsync();
            MapCustomerDetailsToDynamic(vm, customerDetails);
        }

        private void MapCustomerDetailsToDynamic(dynamic vm, CustomersRegistryDetailsModel customerDetails)
        {
            vm.CustomerFirstName = customerDetails.CustomerFirstName;
            vm.CustomerLastName = customerDetails.CustomerLastName;
            vm.CustomerAddress = customerDetails.CustomerAddress;
            vm.CustomerCity = customerDetails.CustomerCity;
            vm.CustomerZipCode = customerDetails.CustomerZipCode;
            vm.CustomerPhoneNumber = customerDetails.CustomerPhoneNumber;
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

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(uri) };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await httpClient.GetAsync(url);
        }
    }
}
