using ITOps.ViewModelComposition.Gateway;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITOps.ViewModelComposition.Mvc
{
    class CompositionActionFilter : IAsyncResultFilter
    {
        IHandleResult defaultHandler = new DefaultResultHandler();
        IEnumerable<IHandleResult> resultHandlers;

        public CompositionActionFilter( IEnumerable<IHandleResult> resultHandlers )
        {
            this.resultHandlers = resultHandlers;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            (var viewModel, var statusCode) = await CompositionHandler.HandleRequest(context.HttpContext);

            var routeData = context.HttpContext.GetRouteData();
            var request = context.HttpContext.Request;

            //matching handlers could be cached by URL
            //per route only 1 result handler is allowed, the owning one
            var handler = resultHandlers
                .Where(a => a.Matches(routeData, request.Method, request))
                .SingleOrDefault() ?? defaultHandler;

            await handler.Handle(context, viewModel, statusCode);

            await next();
        }
    }
}
