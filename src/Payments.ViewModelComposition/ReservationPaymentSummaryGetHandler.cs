using ITOps.ViewModelComposition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Payments.ViewModelComposition
{
    using System;

    class ReservationPaymentSummaryGetHandler : IHandleRequests
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

        public Task HandleAsync(dynamic vm, RouteData routeData, HttpRequest request)
        {
            var reservationId = request.Query["rid"];

            /*
             * Get reservation details from WebAPI
             */
            // mock data
            vm.CardNumber = "4580471162187448";
            vm.ExpieryDateMonth = "09";
            vm.ExpieryDateYear = "20";
            vm.CCV = "***";
            vm.PaymentAmount = "250";
            vm.PaymentId = Guid.NewGuid();
            vm.CardType = "Visa";

            return Task.CompletedTask;
        }
    }
}
