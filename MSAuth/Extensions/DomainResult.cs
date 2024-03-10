using Microsoft.AspNetCore.Mvc;
using MSAuth.Domain.Notifications;
using System.Net;

namespace MSAuth.API.Extensions
{
    public class DomainResult<T>
    {
        private readonly NotificationContext _notificationContext;
        public T Value { get; set; }

        internal DomainResult(T value, NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
            Value = value;
        }

        public static IActionResult Ok(T value, NotificationContext notificationContext)
        {
            return new ObjectResult(null)
            {
                StatusCode = (int)HttpStatusCode.OK,
                Value = new DomainResult<T>(value, notificationContext)
            };
        }

        public static IActionResult Failure(T errorMessage, NotificationContext notificationContext, HttpStatusCode statusCode)
        {
            DomainResult<T> result = new(errorMessage, notificationContext);

            return new ObjectResult(null)
            {
                StatusCode = (int)statusCode,
                Value = result
            };
        }
    }
}
