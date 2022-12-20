using FluentValidation;
using Reviews.CommandApi.Core.Models.Requests;

namespace Reviews.CommandApi.Core.Validators.Models.Requests
{
    public class ReviewRequestValidator : AbstractValidator<ReviewRequest>
    {
        public ReviewRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("A review Id must be specified.");

            RuleFor(x => x.MovieId)
                .NotNull()
                .NotEqual(Guid.Empty)
                .WithMessage("A review movieId must be specified.");

            RuleFor(x => x.Title)
                .Length(1, 100)
                .WithMessage("A review title must be greater than 1 and less than 100.");

            RuleFor(x => x.Message)
                .Length(100, 3000)
                .WithMessage("A review message must be greater than 100 and less than 3000.");

            RuleFor(x => x.CreatedAt)
                .NotNull()
                .NotEqual(DateTime.MinValue)
                .WithMessage("A review date must be specified.");

            RuleFor(x => x.CreatedBy)
                .NotNull()
                .NotEmpty()
                .WithMessage("A review created must be specified.");
        }
    }
}
