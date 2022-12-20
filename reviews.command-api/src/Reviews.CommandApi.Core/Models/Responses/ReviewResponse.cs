namespace Reviews.CommandApi.Core.Models.Responses
{
    public record ReviewResponse : BaseResponse
    {
        public override string Type => "review-response";
        public string Message { get; set; }

        public static ReviewResponse From(Guid reviewId) => new()
        {
            Id = reviewId,
            ResponseDate = DateTime.Now,
            Message = "Review successfully created.",
        };
    }
}
