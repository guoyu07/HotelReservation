namespace Payments.ViewModelComposition
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
    
    partial class NewReservationPaymentPostHandler : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsPost(httpVerb)
                && controller.ToLowerInvariant() == "reservation"
                && action.ToLowerInvariant() == "new";
        }

        public Task HandleAsync(dynamic vm, RouteData routeData, HttpRequest request)
        {
            var form = request.Form;
            var paymentDetails = ReservationPaymentDetailsModelMapper.MapFormToPaymentDetailsModel(form);
            paymentDetails.PaymentId = Guid.NewGuid().ToString();

            PostReservationPaymentDetails(paymentDetails).Wait();

            return Task.CompletedTask;
        }

        private async Task PostReservationPaymentDetails(ReservationPaymentDetailsModel reservationPaymentDetails)
        {
            HttpContent jasonHttpContent = new StringContent(JsonConvert.SerializeObject(reservationPaymentDetails),
                Encoding.UTF8, "application/json");

            var result = await ReservationPaymentWriteApiTask(jasonHttpContent);

            HttpStatusCode resultStatusCode = result.StatusCode;

            if (resultStatusCode == HttpStatusCode.OK)
            {
            }

            if (resultStatusCode != HttpStatusCode.OK)
            {
                // issues? 
            }
        }

        private async Task<HttpResponseMessage> ReservationPaymentWriteApiTask(HttpContent content)
        {
            const string uri = "http://localhost:8182";
            const string url = "/api/paymentswrite";

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(uri) };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await httpClient.PostAsync(url, content);
        }
    }
}
