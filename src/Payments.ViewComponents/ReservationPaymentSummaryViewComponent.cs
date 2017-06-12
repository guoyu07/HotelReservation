using Microsoft.AspNetCore.Mvc;

namespace Payments.ViewComponents
{
    public class ReservationPaymentSummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
