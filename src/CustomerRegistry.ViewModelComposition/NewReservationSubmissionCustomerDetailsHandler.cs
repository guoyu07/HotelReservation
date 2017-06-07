using ITOps.ViewModelComposition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace CustomersRegistry.ViewModelComposition
{
    class NewReservationSubmissionCustomerDetailsHandler : IHandleRequests
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
            var rid = request.Form["ReservationId"][0];
            /*
             * Get Customer details from incoming FORM
             * and post them to Customers Registry API
             */

            return Task.CompletedTask;
        }
    }
}
