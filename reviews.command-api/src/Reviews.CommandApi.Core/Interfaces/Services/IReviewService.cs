using Reviews.CommandApi.Core.Models.Requests;
using Reviews.CommandApi.Core.Models.Responses;

namespace Reviews.CommandApi.Core.Interfaces.Services
{
    public interface IReviewService
    {
        Task<ReviewResponse> RegisterReview(ReviewRequest request, CancellationToken cancellationToken = default);
    }
}
