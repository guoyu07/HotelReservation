using Microsoft.AspNetCore.Mvc;

namespace Payments.ViewComponents
{
    public class ReservationPaymentFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
