using Microsoft.AspNetCore.Mvc;

namespace Reservations.ViewComponents
{
    public class ReservationCancelSummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
