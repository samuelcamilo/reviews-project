namespace Reviews.CommandApi.Core.Entities.Responses
{
    public record ReviewResponse : ResponseBase
    {
        public override string Type { get; } = "review-response";

        public static ReviewResponse From(Guid reviewId) => new()
        {
            Id = reviewId,
            ResponseDate = DateTime.Now,
            Message = "Review successfully created.",
        };
    }
}
