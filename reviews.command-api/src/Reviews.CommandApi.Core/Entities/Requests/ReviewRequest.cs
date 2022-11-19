namespace Reviews.CommandApi.Core.Entities.Requests
{
    public record ReviewRequest
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid MovieId { get; init; }
        public string Title { get; init; }
        public string Message { get; init; }
        public DateTime CreatedAt { get; init; }
        public string CreatedBy { get; init; }
    }
}
