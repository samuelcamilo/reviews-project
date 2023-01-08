using FluentValidation.Results;
using Reviews.CommandApi.Core.Interfaces.Notifications;
using Reviews.CommandApi.Core.Notifications;

namespace Reviews.CommandApi.Core.Models.Responses;

public record ErrorResponse : BaseResponse
{
    public override string Type { get; } = "error-response";
    public List<object> Errors { get; set; }

    public static ErrorResponse FromValidators(ValidationResult validationResult)
    {
        var errors = validationResult.Errors
            .Select(err => new
            {
                Title = err.PropertyName,
                Detail = err.ErrorMessage
            })
            .ToList<object>();

        return BuildError(errors);
    }

    public static ErrorResponse FromNotification(INotification notification)
    {
        var errors = new List<object>()
        {
            new 
            {
                Detail = notification.GetSummary(),
            }
        };

        return BuildError(errors);
    }

    public static ErrorResponse FromException(Exception ex)
    {
        var errors = new List<object>()
        {
            new
            {
                Detail = ex.Message,
            }
        };

        return BuildError(errors);
    }

    public static ErrorResponse BuildError(List<object> errors) =>
        new()
        {
            Id = Guid.NewGuid(),
            ResponseDate = DateTime.Now,
            Errors = errors,
        };
}