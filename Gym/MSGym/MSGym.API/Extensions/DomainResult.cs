using Microsoft.AspNetCore.Mvc;
using MSGym.Domain.ModelErrors;
using MSGym.Domain.Notifications;
using System.Net;

namespace MSGym.API.Extensions
{
    public class DomainResult<T>
    {
        private readonly NotificationContext _notificationContext;
        private readonly ModelErrorsContext _modelErrorsContext;

        public Dictionary<string, string> Notifications => _notificationContext.Notifications.ToDictionary(e => e.Error, e => e.Message);
        public List<ModelErrors> ModelErrors => _modelErrorsContext.ModelErrors.ToList();
        public bool Success => !_notificationContext.HasNotifications && !_modelErrorsContext.HasErrors;
        public T Value { get; set; }

        internal DomainResult(T value, NotificationContext notificationContext, ModelErrorsContext modelErrorsContext)
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            Value = value;
        }

        public static IActionResult Ok(T value, NotificationContext notificationContext, ModelErrorsContext modelErrorsContext)
        {
            return new ObjectResult(null)
            {
                StatusCode = (int)HttpStatusCode.OK,
                Value = new DomainResult<T>(value, notificationContext, modelErrorsContext)
            };
        }

        public static IActionResult Failure(T errorMessage, NotificationContext notificationContext, ModelErrorsContext modelErrorsContext, HttpStatusCode statusCode)
        {
            DomainResult<T> result = new(errorMessage, notificationContext, modelErrorsContext);

            return new ObjectResult(null)
            {
                StatusCode = (int)statusCode,
                Value = result
            };
        }
    }
}
