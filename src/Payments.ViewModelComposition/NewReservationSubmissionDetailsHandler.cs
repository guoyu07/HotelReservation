using ITOps.ViewModelComposition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Payments.ViewModelComposition
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using Newtonsoft.Json;

    // rename this to NewPaymentSubmissionDetailsHandler??
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
            var form = request.Form;
            //var rid = request.Form["ReservationId"][0];

            //var reservationId = new Guid(rid);

            /*
             * using credit card details from the FORM
             * post them to the Payments WebAPI to start
             * the payment process
             */

            var PaymentDetails = MapFormToPaymentDetailsModel(form);

            PostReservationDetails(PaymentDetails).Wait();
            return Task.CompletedTask;
        }

        private async Task PostReservationDetails(ReservationPaymentDetailsModel reservationDetails)
        {
            HttpContent jasonHttpContent = new StringContent(JsonConvert.SerializeObject(reservationDetails),
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

        private ReservationPaymentDetailsModel MapFormToPaymentDetailsModel(IFormCollection form)
        {
            return new ReservationPaymentDetailsModel
            {
                ReservationId = form["ReservationId"],
                
                CardNumber = form["CardNumber"],
                ExpieryDateMonth = form["ExpieryDateMonth"],
                ExpieryDateYear = form["ExpieryDateYear"],
                CCV = form["CCV"],
                PaymentAmount = form["PaymentAmount"],

                CustomerId = form["CustomerId"],
            };
        }

        internal class ReservationPaymentDetailsModel
        {
            public string ReservationId { get; set; }
            public string CustomerId { get; set; }
            public string CardNumber { get; set; }
            public string ExpieryDateMonth { get; set; }
            public string ExpieryDateYear { get; set; }
            public string CCV { get; set; }
            public string PaymentAmount { get; set; }
        }
    }
}
