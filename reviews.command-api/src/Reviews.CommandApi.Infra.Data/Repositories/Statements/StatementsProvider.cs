using Reviews.CommandApi.Core.Entities;

namespace Reviews.CommandApi.Infra.Data.Repositories.Statements
{
    public static class StatementsProvider
    {
        public static object GetInsertParameters(ReviewEntity review) =>
            new
            {
                review.Id,
                review.MovieId,
                review.Title,
                review.Message,
                review.CreatedAt,
                review.CreatedBy,
            };
    }
}
