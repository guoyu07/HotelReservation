using ITOps.ViewModelComposition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Payments.ViewModelComposition
{
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

            return Task.CompletedTask;
        }
    }
}
