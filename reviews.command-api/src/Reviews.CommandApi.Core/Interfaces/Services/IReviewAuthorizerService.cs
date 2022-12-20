using Reviews.CommandApi.Core.Models.Requests;

namespace Reviews.CommandApi.Core.Interfaces.Services
{
    public interface IReviewAuthorizerService
    {
        Task<bool> AuthorizeAsync(ReviewRequest request, CancellationToken cancellationToken);
    }
}
