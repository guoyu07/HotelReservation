using Microsoft.AspNetCore.Mvc;

namespace Reservations.ViewComponents
{
    public class ReservationDetailsFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
