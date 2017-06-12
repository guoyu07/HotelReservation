using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Reservations.ViewComponents
{
    public class ReservationController : Controller
    {
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(IFormCollection form)
        {
            return View();
        }

        public IActionResult Details(Guid rid)
        {
            return View();
        }
    }
}