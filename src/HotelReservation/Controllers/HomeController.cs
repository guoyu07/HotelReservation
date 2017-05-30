using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}