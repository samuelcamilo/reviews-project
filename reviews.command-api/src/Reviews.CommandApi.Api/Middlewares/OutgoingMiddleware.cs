using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Reviews.CommandApi.Core.Constants;
using Reviews.CommandApi.Core.Interfaces.Notifications;
using Reviews.CommandApi.Core.Models.Responses;

namespace Reviews.CommandApi.Api.Middlewares
{
    public class OutgoingMiddleware : IExceptionFilter, IActionFilter
    {
        private readonly INotification _notification;

        public OutgoingMiddleware(INotification notification) =>
            _notification = notification;

        public void OnException(ExceptionContext context)
        {
            var errorResponse = ErrorResponse.FromException(context.Exception);

            context.ExceptionHandled = true;
            context.Result = new ObjectResult(errorResponse);

            context.HttpContext.Response.StatusCode = ResponseCodes.InternalServerError;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_notification.Any())
            {
                var errorResponse = ErrorResponse.FromNotification(_notification);

                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = _notification.GetStatusCode(),
                };

                return;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // logger something...
        }
    }
}
