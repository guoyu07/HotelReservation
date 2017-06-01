using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace ITOps.ViewModelComposition.Mvc
{
    class DefaultResultHandler : IHandleResult
    {
        public Task Handle(ResultExecutingContext context, dynamic viewModel, int httpStatusCode)
        {
            if (context.Result is ViewResult viewResult && viewResult.ViewData.Model == null)
            {
                //MVC
                if (httpStatusCode == StatusCodes.Status200OK)
                {
                    viewResult.ViewData.Model = viewModel;
                }
            }
            else if (context.Result is ObjectResult objectResult && objectResult.Value == null)
            {
                //WebAPI
                if (httpStatusCode == StatusCodes.Status200OK)
                {
                    objectResult.Value = viewModel;
                }
            }

            return Task.CompletedTask;
        }

        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request) => true;
    }
}
