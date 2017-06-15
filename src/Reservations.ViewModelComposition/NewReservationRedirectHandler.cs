using ITOps.ViewModelComposition.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Reservations.ViewModelComposition
{
    class NewReservationRedirectHandler : IHandleResult
    {
        public Task Handle(ResultExecutingContext context, dynamic viewModel, int httpStatusCode)
        {
            if (context.Result is ViewResult viewResult && viewResult.ViewData.Model == null)
            {
                //MVC
                if (httpStatusCode == StatusCodes.Status200OK)
                {
                    context.Result = new RedirectToActionResult("Summary", "Reservation", new { rid = viewModel.ReservationId });
                }
            }

            return Task.CompletedTask;
        }

        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            //this is the POST interceptor when reservation is submitted
            var controller = (string)routeData.Values["controller"];
            var action = (string)routeData.Values["action"];

            return HttpMethods.IsPost(httpVerb)
                && controller.ToLowerInvariant() == "reservation"
                && action.ToLowerInvariant() == "new";
        }
    }
}
