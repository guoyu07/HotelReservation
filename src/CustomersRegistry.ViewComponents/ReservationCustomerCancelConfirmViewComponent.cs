namespace CustomersRegistry.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    public class ReservationCustomerCancelConfirmViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
