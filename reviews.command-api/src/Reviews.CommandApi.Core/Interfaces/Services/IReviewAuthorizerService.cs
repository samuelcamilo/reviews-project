using Reviews.CommandApi.Core.Entities.Requests;
using Reviews.CommandApi.Shared;

namespace Reviews.CommandApi.Core.Interfaces.Services
{
    public interface IReviewAuthorizerService
    {
        Task<OperationResult> AuthorizeAsync(ReviewRequest request);
    }
}
