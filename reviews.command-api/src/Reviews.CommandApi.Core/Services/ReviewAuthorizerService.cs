using Reviews.CommandApi.Core.Entities.Requests;
using Reviews.CommandApi.Core.Interfaces.Services;
using Reviews.CommandApi.Shared;

namespace Reviews.CommandApi.Core.Services
{
    public class ReviewAuthorizerService : IReviewAuthorizerService
    {
        public Task<OperationResult> AuthorizeAsync(ReviewRequest request)
        {
            // TODO...
            return OperationResult.Success();
        }
    }
}
