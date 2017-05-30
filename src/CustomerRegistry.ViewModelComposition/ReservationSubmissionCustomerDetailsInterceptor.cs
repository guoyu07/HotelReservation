using ITOps.ViewModelComposition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomersRegistry.ViewModelComposition
{
    class ReservationSubmissionCustomerDetailsInterceptor : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            //this is the POST interceptor when reservation is submitted
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsPost(httpVerb)
                && controller.ToLowerInvariant() == "reservation"
                && action.ToLowerInvariant() == "submit";
        }

        public Task Handle(dynamic vm, RouteData routeData, HttpRequest request)
        {
            var form = request.Form;
            /*
             * Get Customer details from incoming FORM
             * and post them to Customers Registry API
             */

            return Task.CompletedTask;
        }
    }
}
