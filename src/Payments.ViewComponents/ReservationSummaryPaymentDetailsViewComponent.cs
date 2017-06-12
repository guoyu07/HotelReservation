using Microsoft.AspNetCore.Mvc;

namespace Payments.ViewComponents
{
    public class ReservationSummaryPaymentDetailsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
