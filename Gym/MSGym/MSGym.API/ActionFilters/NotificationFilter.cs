using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MSGym.Domain.Notifications;

namespace MSGym.API.ActionFilters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly NotificationContext _notificationContext;

        public NotificationFilter(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            ObjectResult objectResult = (ObjectResult)context.Result;
            if (_notificationContext.HasNotifications)
            {
                objectResult.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            await next();
        }
    }
}
