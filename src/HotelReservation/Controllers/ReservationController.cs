using Microsoft.AspNetCore.Mvc;

namespace HotelReservation
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit()
        {
            return View();
        }
    }
}