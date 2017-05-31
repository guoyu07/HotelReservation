using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomersRegistry.ViewComponents
{
    public class ReservationSummaryCustomerDetailsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic model)
        {
            return View(model);
        }
    }
}
