using Reviews.CommandApi.Core.Models.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace Reviews.CommandApi.Api.Examples.Requests
{
    internal class ReviewRequestExample : IExamplesProvider<ReviewRequest>
    {
        public ReviewRequest GetExamples() => new()
        {
            Id = Guid.NewGuid(),
            MovieId = Guid.Parse("db10c40e-9fd0-44dd-a077-e16ededcb60d"),
            Title = "Harry Potter and the Deathly Hallows – Part 1",
            Message = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
            "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, " +
            "but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
            "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
            CreatedAt = DateTime.Now,
            CreatedBy = "Swagger Example",
        };
    }
}
