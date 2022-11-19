namespace Reviews.CommandApi.Core.Entities
{
    public record ReviewEntity
    {
        public Guid Id { get; init; }
        public Guid MovieId { get; init; }
        public string Title { get; init; }
        public string Message { get; init; }
        public DateTime CreatedAt { get; init; }
        public string CreatedBy { get; init; }
    }
}
