using Reviews.CommandApi.Core.Entities;
using Reviews.CommandApi.Core.Entities.Requests;
using Reviews.CommandApi.Core.Entities.Responses;
using Reviews.CommandApi.Core.Factories;
using Reviews.CommandApi.Core.Interfaces.Data;
using Reviews.CommandApi.Core.Interfaces.Services;
using static Reviews.CommandApi.Shared.OperationResult;
using Reviews.CommandApi.Shared.Exceptions;
using Reviews.CommandApi.Shared;

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

        public async Task<OperationResult<ReviewResponse>> RegisterReview(
            ReviewRequest request, 
            CancellationToken cancellationToken = default)
        {
            var authorized = await _reviewAuthorizerService
                .AuthorizeAsync(request);

            if (!authorized.IsSuccess)
                throw new NotAuthorizedReviewException();

            var authorizedReview = ReviewEntityFactory.CreateFrom(request);
            await RegisterAuthorizedReviewAsync(authorizedReview, cancellationToken);

            return Success(ReviewResponse.From(request.Id));
        }

        private async Task<OperationResult> RegisterAuthorizedReviewAsync(
            ReviewEntity review,
            CancellationToken cancellationToken)
        {
            _uow.BeginTransaction();

            try
            {
                await _reviewRepository.Insert(review);
                _uow.Commit();
 
                return Success();
            }
            catch (Exception)
            {
                _uow.Rollback();
                throw;
            }
        }
    }
}
