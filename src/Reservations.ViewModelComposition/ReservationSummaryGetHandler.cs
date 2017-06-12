namespace Reservations.ViewModelComposition
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using ITOps.ViewModelComposition;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Model;
    using Newtonsoft.Json;

    class ReservationSummaryGetHandler : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            //this is the reservation details after a reservation has been submitted
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsGet(httpVerb)
                && controller.ToLowerInvariant() == "reservation"
                && action.ToLowerInvariant() == "summary";
        }

        public async Task HandleAsync(dynamic vm, RouteData routeData, HttpRequest request)
        {
            var reservationId = request.Query["rid"];

            /*
             * Get reservation details from WebAPI
             */
            var reservationDetails = await GetReservationDetailsAsync(reservationId);

            vm.ReservationId = reservationId;

            MapCustomerDetailsToDynamic(vm, reservationDetails);
        }

        public static void MapCustomerDetailsToDynamic(dynamic vm, ReservationDetailsModel reservationDetails)
        {
            vm.ReservationId = reservationDetails.ReservationId;
            vm.CustomerId = reservationDetails.CustomerId;
            vm.HotelId = reservationDetails.HotelId;
            vm.CheckIn = reservationDetails.CheckIn;
            vm.CheckOut = reservationDetails.CheckOut;
            vm.PayAtHotel = reservationDetails.PayAtHotel;
            vm.PaymentId = reservationDetails.PaymentId;
            vm.CustomerComments = reservationDetails.CustomerComments;
            vm.HotelName = reservationDetails.HotelName;
        }

        private async Task<ReservationDetailsModel> GetReservationDetailsAsync(string reservationId)
        {
            var result = await CustomerReadAPIAsync(reservationId);
            return JsonConvert.DeserializeObject<ReservationDetailsModel>(await result.Content.ReadAsStringAsync());
        }

        private async Task<HttpResponseMessage> CustomerReadAPIAsync(string reservationId)
        {
            const string uri = "http://localhost:8183";
            const string url = "/api/reservationsread";

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(uri) };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await httpClient.GetAsync(string.Format(url + "/reservationId={0}", reservationId));
        }
    }
}
