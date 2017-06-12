using Microsoft.AspNetCore.Mvc;

namespace Reservations.ViewComponents
{
    public class ReservationSummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
