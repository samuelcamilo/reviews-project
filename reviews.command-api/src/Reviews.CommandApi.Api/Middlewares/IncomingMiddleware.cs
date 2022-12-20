using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Reviews.CommandApi.Core.Constants;
using Reviews.CommandApi.Core.Models.Responses;

namespace Reviews.CommandApi.Api.Middlewares
{
    public class IncomingMiddleware : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Items.TryGetValue(nameof(ValidationResult), out var value))
            {
                var errorResponse = ErrorResponse.FromValidators((ValidationResult)value);

                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = ResponseCodes.UnprocessableEntity,
                };

                return;
            }

            await next();
        }
    }
}
