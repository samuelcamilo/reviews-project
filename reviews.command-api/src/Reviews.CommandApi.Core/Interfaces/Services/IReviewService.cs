using Reviews.CommandApi.Core.Entities.Requests;
using Reviews.CommandApi.Core.Entities.Responses;
using Reviews.CommandApi.Shared;

namespace Reviews.CommandApi.Core.Interfaces.Services
{
    public interface IReviewService
    {
        Task<OperationResult<ReviewResponse>> RegisterReview(ReviewRequest request, CancellationToken cancellationToken = default);
    }
}
