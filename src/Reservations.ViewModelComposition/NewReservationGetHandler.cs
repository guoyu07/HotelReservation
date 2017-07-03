using ITOps.ViewModelComposition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Reservations.ViewModelComposition
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.InteropServices;
    using Model;
    using Newtonsoft.Json;

    class NewReservationGetHandler : IHandleRequests
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
            /*
             * Add reservation details here
             * such as Hotel and check-in/out dates
             */
            var mockResrrvationId = Guid.Parse("976c44a6-20df-4af7-b1ca-70798ab9df41");

            var reservationDetails = await GetReservationReservationDetailsAsync(mockResrrvationId.ToString());
            MapReservationDetailsToDynamic(vm, reservationDetails, mockResrrvationId.ToString());
        }

        private async Task<ReservationDetailsModel> GetReservationReservationDetailsAsync(string reservationId)
        {
            var result = await ReservationsReadAPIAsync(reservationId);
            // TODO: check that resutlt has items...

            return JsonConvert.DeserializeObject<IList<ReservationDetailsModel>>(await result.Content.ReadAsStringAsync())[0];
        }

        private async Task<HttpResponseMessage> ReservationsReadAPIAsync(string paymetId)
        {
            const string uri = "http://localhost:8183";
            const string url = "/api/reservationsread";

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(uri) };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await httpClient.GetAsync(string.Format(url + "/{0}", paymetId));
        }

        public static void MapReservationDetailsToDynamic(dynamic vm, ReservationDetailsModel reservationDetails, string reservationId)
        {
            vm.ReservationId = Guid.NewGuid(); // new reservation Id
            vm.CustomerId = reservationDetails.CustomerId;
            vm.HotelId = reservationDetails.HotelId;
            vm.CheckIn = reservationDetails.CheckIn;
            vm.CheckOut = reservationDetails.CheckOut;
            vm.PayAtHotel = reservationDetails.PayAtHotel;
            vm.PayNow = reservationDetails.PayNow;
            vm.PaymentId = reservationDetails.PaymentId;
            vm.CustomerComments = reservationDetails.CustomerComments;
            vm.HotelName = reservationDetails.HotelName;
            vm.UiState = reservationDetails.UiState;
        }
    }
}
