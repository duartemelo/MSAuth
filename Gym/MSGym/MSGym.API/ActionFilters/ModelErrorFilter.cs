using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MSGym.Domain.ModelErrors;

namespace MSGym.API.ActionFilters
{
    public class ModelErrorFilter : IAsyncResultFilter
    {
        private readonly ModelErrorsContext _modelErrorContext;

        public ModelErrorFilter(ModelErrorsContext modelErrorContext)
        {
            _modelErrorContext = modelErrorContext;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            ObjectResult objectResult = (ObjectResult)context.Result;
            if (_modelErrorContext.HasErrors)
            {
                objectResult.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            await next();
        }
    }
}
