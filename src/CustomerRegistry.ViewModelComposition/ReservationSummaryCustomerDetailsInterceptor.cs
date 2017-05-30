using ITOps.ViewModelComposition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomersRegistry.ViewModelComposition
{
    class ReservationSummaryCustomerDetailsInterceptor : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            //this is the reservation summary before final confirmation by the user
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsGet(httpVerb)
                && controller.ToLowerInvariant() == "reservation"
                && action.ToLowerInvariant() == "summary";
        }

        public Task Handle(dynamic vm, RouteData routeData, HttpRequest request)
        {
            if (request.HttpContext.User.Identity.IsAuthenticated)
            {
                /*
                 * try read customer info from backend API 
                 * based on current user id and pre-fill 
                 * the submission form
                 */
            }
            else
            {
                /*
                 * add to vm an empty customer model 
                 * ready to be filled in the form
                 */
            }

            return Task.CompletedTask;
        }
    }
}
