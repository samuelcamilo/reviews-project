using Reviews.CommandApi.Core.Entities;
using Reviews.CommandApi.Core.Factories;
using Reviews.CommandApi.Core.Interfaces.Data;
using Reviews.CommandApi.Core.Interfaces.Services;
using Reviews.CommandApi.Core.Models.Requests;
using Reviews.CommandApi.Core.Models.Responses;

namespace Reviews.CommandApi.Core.Services
{
    public record ReviewService : IReviewService
    {
        private readonly IReviewAuthorizerService _reviewAuthorizerService;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _uow;

        public ReviewService(
            IReviewAuthorizerService reviewAuthorizerService,
            IReviewRepository reviewRepository, 
            IUnitOfWork uow)
        {
            _reviewAuthorizerService = reviewAuthorizerService;
            _reviewRepository = reviewRepository;
            _uow = uow;
        }

        public async Task<ReviewResponse> RegisterReview(
            ReviewRequest request, 
            CancellationToken cancellationToken = default)
        {
            var authorized = await _reviewAuthorizerService
                .AuthorizeAsync(request, cancellationToken);

            if (!authorized)
                return default;

            var authorizedReview = ReviewEntityFactory.CreateFrom(request);
            await RegisterAuthorizedReviewAsync(authorizedReview, cancellationToken);

            return ReviewResponse.From(request.Id);
        }

        private async Task<bool> RegisterAuthorizedReviewAsync(
            ReviewEntity review,
            CancellationToken cancellationToken)
        {
            _uow.BeginTransaction();

            try
            {
                await _reviewRepository.Insert(review);
                _uow.Commit();

                return true;
            }
            catch (Exception)
            {
                _uow.Rollback();
                throw;
            }
        }
    }
}
