using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CustomersRegistry.ViewComponents
{
    public class ReservationSummaryCustomerDetailsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(dynamic model)
        {
            await Task.CompletedTask;
            return View(model);
        }
    }
}
