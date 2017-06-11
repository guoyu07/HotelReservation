using Microsoft.AspNetCore.Mvc;

namespace Payments.ViewComponents
{
    public class ReservationSummaryViewPaymentDetailsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
