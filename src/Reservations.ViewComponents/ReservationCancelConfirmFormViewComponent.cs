using Microsoft.AspNetCore.Mvc;

namespace Reservations.ViewComponents
{
    public class ReservationCancelConfirmFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
