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
    using Newtonsoft.Json;

    class NewReservationSubmissionDetailsHandler : IHandleRequests
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
            // * TODO:
            // * Get reservation details from incoming FORM
            // * and post them to Reservations write API
            // */

            var form = request.Form;
            //var rid = request.Form["ReservationId"][0];
            //vm.ReservationId = new Guid(rid);

            var reservationDetails = MapFormToReservationDetails(form);

            PostReservationDetails(reservationDetails).Wait();

            return Task.CompletedTask;
        }

        private async Task PostReservationDetails(ReservationDetailsModel reservationDetails)
        {
            HttpContent jasonHttpContent = new StringContent(JsonConvert.SerializeObject(reservationDetails),
                Encoding.UTF8, "application/json");

            var result = await ReservationsWriteApiTask(jasonHttpContent);

            HttpStatusCode resultStatusCode = result.StatusCode;

            if (resultStatusCode == HttpStatusCode.OK)
            {
            }

            if (resultStatusCode != HttpStatusCode.OK)
            {
                // issues? 
            }
        }

        private async Task<HttpResponseMessage> ReservationsWriteApiTask(HttpContent content)
        {
            const string uri = "http://localhost:8183";
            const string url = "/api/reservationswrite";

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(uri) };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await httpClient.PostAsync(url, content);
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

    internal class ReservationDetailsModel
    {
        public string ReservationId { get; set;}
        public string CustomerId { get; set; } 
        public string HotelId { get; set; } 
        public string CheckIn { get; set; } 
        public string CheckOut { get; set; } 
        public string PayAtHotel { get; set; } 
        public string PaymentId { get; set; } 
        public string CustomerComments { get; set; }
        public string PayNow { get; set; }
        public string HotelName { get; set; }
    }
}
