using Reviews.CommandApi.Core.Entities;
using Reviews.CommandApi.Core.Entities.Requests;

namespace Reviews.CommandApi.Core.Factories
{
    public static class ReviewEntityFactory
    {
        public static ReviewEntity CreateFrom(ReviewRequest request)
        {
            return new()
            {
                Id = request.Id,
                MovieId = request.MovieId,
                Title = request.Title,
                Message = request.Message,
                CreatedAt = DateTime.Now,
                CreatedBy = string.Empty,
            };
        }
    }
}
