namespace Reservations.ViewModelComposition
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using ITOps.ViewModelComposition;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Model;
    using Newtonsoft.Json;

    class ReservationCancelPostHandler : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            //this is the reservation details after a reservation has been submitted
            var controller = (string) routeData.Values["controller"];
            var action = (string) routeData.Values["action"];

            return HttpMethods.IsPost(httpVerb)
                   && controller.ToLowerInvariant() == "reservation"
                   && action.ToLowerInvariant() == "cancel";
        }

        public Task HandleAsync(dynamic vm, RouteData routeData, HttpRequest request)
        {
            string reservationId = request.Query["rid"];

            if (reservationId == null)
            {
                reservationId = request.Form["reservationId"];
            }

            /*
             * Get reservation details from WebAPI
             */
            var form = request.Form;
            var reservationDetails = MapFormToReservationDetails(form);
            PostReservationCancelation(reservationDetails, reservationId).Wait();

            vm.ReservationId = reservationId;

            return Task.CompletedTask;
        }


        private async Task PostReservationCancelation(ReservationDetailsModel reservationDetails, string reservationId)
        {
            HttpContent jasonHttpContent = new StringContent(JsonConvert.SerializeObject(reservationDetails),
                Encoding.UTF8, "application/json");

            var result = await ReservationsCancelWriteApiTask(jasonHttpContent, reservationId);
            HttpStatusCode resultStatusCode = result.StatusCode;

            if (resultStatusCode == HttpStatusCode.OK)
            {
            }

            if (resultStatusCode != HttpStatusCode.OK)
            {
                // issues? 
            }
        }

        private async Task<HttpResponseMessage> ReservationsCancelWriteApiTask(HttpContent content,
            string reservationId)
        {
            const string uri = "http://localhost:8183";
            const string url = "/api/reservationswrite";

            HttpClient httpClient = new HttpClient {BaseAddress = new Uri(uri)};

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string postUrl = string.Format(url + "/{0}/cancel", reservationId);
            return await httpClient.PostAsync(postUrl, content);
        }

        private ReservationDetailsModel MapFormToReservationDetails(IFormCollection form)
        {
            return new ReservationDetailsModel
            {
                ReservationId = form["ReservationId"],
                CustomerId = form["CustomerId"],
                HotelId = form["HotelId"],
                HotelName = form["HotelName"],
                CheckIn = form["CheckIn"],
                CheckOut = form["CheckOut"],
                PayAtHotel = form["PayAtHotel"],
                PayNow = form["PayNow"],
                CustomerComments = form["CustomerComments"],
            };
        }
    }
}
