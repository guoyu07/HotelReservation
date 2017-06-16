using Microsoft.AspNetCore.Mvc;

namespace Payments.ViewComponents
{
    public class ReservationPaymentCancelConfirmViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
