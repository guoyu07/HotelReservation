namespace Payments.ViewModelComposition
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

    class NewReservationPaymentGetHandler : IHandleRequests
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
             * pre-fill available data, if any, about user credit card
             */

            var mockpaymentId = Guid.Parse("5f2f6ae6-a8e0-490b-81d9-78469fee677c");

            var paymentDetails = await GetReservationPaymentDetailsAsync(mockpaymentId.ToString());
            MapPaymentDetailsToDynamic(vm, paymentDetails);
        }

        private async Task<ReservationPaymentDetailsModel> GetReservationPaymentDetailsAsync(string paymetId)
        {
            var result = await PaymentsReadAPIAsync(paymetId);
            return JsonConvert.DeserializeObject<IList<ReservationPaymentDetailsModel>>(await result.Content.ReadAsStringAsync())[0];
        }

        private async Task<HttpResponseMessage> PaymentsReadAPIAsync(string paymetId)
        {
            const string uri = "http://localhost:8182";
            const string url = "/api/paymentsread";

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri(uri) };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return await httpClient.GetAsync(string.Format(url + "/{0}", paymetId));
        }

        private void MapPaymentDetailsToDynamic(dynamic vm, ReservationPaymentDetailsModel paymentDetailsDetails)
        {
            vm.PaymentAmount = paymentDetailsDetails.PaymentAmount;
            vm.CardNumber = paymentDetailsDetails.CardNumber;
            vm.ExpieryDateMonth = paymentDetailsDetails.ExpieryDateMonth;
            vm.ExpieryDateYear = paymentDetailsDetails.ExpieryDateYear;
            vm.CCV = paymentDetailsDetails.CCV;
            vm.PaymentId = paymentDetailsDetails.PaymentId;
            vm.CardType = paymentDetailsDetails.CardType;
        }
    }
}
