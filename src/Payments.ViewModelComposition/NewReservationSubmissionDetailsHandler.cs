using ITOps.ViewModelComposition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Payments.ViewModelComposition
{
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
            var rid = request.Form["ReservationId"][0];

            var reservationId = new Guid(rid);

            /*
             * using credit card details from the FORM
             * post them to the Payments WebAPI to start
             * the payment process
             */

            return Task.CompletedTask;
        }
    }
}
