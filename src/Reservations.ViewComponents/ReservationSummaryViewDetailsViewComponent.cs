using Microsoft.AspNetCore.Mvc;

namespace Reservations.ViewComponents
{
    public class ReservationSummaryViewDetailsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
