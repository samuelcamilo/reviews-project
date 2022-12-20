using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Reviews.CommandApi.Api.Middlewares
{
    public class ValidatorMiddleware : IValidatorInterceptor
    {
        public ValidationResult AfterAspNetValidation(
            ActionContext actionContext, 
            IValidationContext validationContext, 
            ValidationResult result)
        {
            if (!result.IsValid)
            {
                actionContext.HttpContext.Items.Add(nameof(ValidationResult), result);
            }

            return result;
        }

        public IValidationContext BeforeAspNetValidation(
            ActionContext actionContext, 
            IValidationContext commonContext) => commonContext;
    }
}
