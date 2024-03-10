using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MSAuth.API.ActionFilters
{
    public class RequireAppKeyAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("AppKey", out var appKey) || string.IsNullOrEmpty(appKey))
            {
                context.Result = new BadRequestObjectResult("AppKey not provided in headers.");
                return;
            }

            // Store the AppKey in the HttpContext for access in controllers
            context.HttpContext.Items["AppKey"] = appKey.ToString();
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No action needed
        }

        
    }
}
