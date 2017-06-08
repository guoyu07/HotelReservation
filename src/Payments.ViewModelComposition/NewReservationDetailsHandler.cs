using ITOps.ViewModelComposition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Payments.ViewModelComposition
{
    using System;

    class NewReservationDetailsHandler : IHandleRequests
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

        public Task HandleAsync(dynamic vm, RouteData routeData, HttpRequest request)
        {
            /*
             * pre-fill available data, if any, about user credit card
             */

            // mock data
            vm.CardNumber = "4580471162187448";
            vm.ExpieryDateMonth = "09";
            vm.ExpieryDateYear = "20";
            vm.CCV = "***";

            vm.CustomerId = Guid.NewGuid();

            return Task.CompletedTask;
        }
    }
}
