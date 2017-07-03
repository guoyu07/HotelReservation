namespace Reservations.ViewModelComposition
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

    class ReservationCancelSummaryGetHandler : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            //this is the reservation details after a reservation has been submitted
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsGet(httpVerb)
                && controller.ToLowerInvariant() == "reservation"
                && action.ToLowerInvariant() == "cancelsummary";
        }

        public async Task HandleAsync(dynamic vm, RouteData routeData, HttpRequest request)
        {
            var reservationId = request.Query["rid"];

            /*
             * Get reservation details from WebAPI
             */
            var reservationDetails = await GetReservationDetailsAsync(reservationId);

            MapReservationDetailsToDynamic(vm, reservationDetails, reservationId);
        }

        public static void MapReservationDetailsToDynamic(dynamic vm, ReservationDetailsModel reservationDetails,string reservationId)
        {
            vm.ReservationId = reservationId;
            vm.CustomerId = reservationDetails.CustomerId;
            vm.HotelId = reservationDetails.HotelId;
            vm.CheckIn = reservationDetails.CheckIn;
            vm.CheckOut = reservationDetails.CheckOut;
            vm.PayAtHotel = reservationDetails.PayAtHotel;
            vm.PaymentId = reservationDetails.PaymentId;
            vm.CustomerComments = reservationDetails.CustomerComments;
            vm.HotelName = reservationDetails.HotelName;
            vm.UiState = reservationDetails.UiState;
        }

        private async Task<ReservationDetailsModel> GetReservationDetailsAsync(string reservationId)
        {
            var result = await ReservationsReadAPIAsync(reservationId);
            return JsonConvert.DeserializeObject<IList<ReservationDetailsModel>>(await result.Content.ReadAsStringAsync())[0];
        }

        private async Task<HttpResponseMessage> ReservationsReadAPIAsync(string reservationId)
        {
            const string uri = "http://localhost:8183";
            const string url = "/api/reservationsread";

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(uri) };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await httpClient.GetAsync(string.Format(url + "/{0}", reservationId));
        }
    }
}
