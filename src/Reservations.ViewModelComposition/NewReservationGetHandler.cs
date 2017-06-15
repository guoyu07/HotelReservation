using ITOps.ViewModelComposition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Reservations.ViewModelComposition
{
    using System.Runtime.InteropServices;

    class NewReservationGetHandler : IHandleRequests
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
             * Add reservation details here
             * such as Hotel and check-in/out dates
             */
            vm.ReservationId = Guid.NewGuid();
            vm.HotelId = Guid.NewGuid();
            vm.HotelName = "Sample Hotel";
            vm.CheckIn = "15/07/2017";
            vm.CheckOut = "17/07/2017";
            vm.PayAtHotel = true;
            vm.PayNow = false;
            vm.CustomerComments = "";

            return Task.CompletedTask;
        }
    }
}
