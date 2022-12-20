namespace Reviews.CommandApi.Infra.Services.Models.Responses
{
    internal record MovieResponse
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Genre { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
