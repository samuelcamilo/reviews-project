namespace Reviews.CommandApi.Core.Models.Requests
{
    public record ReviewRequest
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid MovieId { get; set; }
        public string Title { get; init; }
        public string Message { get; init; }
        public DateTime CreatedAt { get; init; }
        public string CreatedBy { get; init; }
    }
}
