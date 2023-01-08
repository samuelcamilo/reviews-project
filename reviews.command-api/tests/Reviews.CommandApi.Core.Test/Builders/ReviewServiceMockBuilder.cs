using Moq;
using Reviews.CommandApi.Core.Entities;
using Reviews.CommandApi.Core.Interfaces.Data;
using Reviews.CommandApi.Core.Interfaces.Services;
using Reviews.CommandApi.Core.Models.Requests;
using Reviews.CommandApi.Core.Services;

namespace Reviews.CommandApi.Core.Test.Builders
{
    public class ReviewServiceMockBuilder
    {
        private Mock<IReviewAuthorizerService> _reviewAuthorizerService;
        private Mock<IReviewRepository> _reviewRepository;
        private Mock<IUnitOfWork> _uow;

        private readonly MockRepository _mocks = new(MockBehavior.Strict);

        public ReviewServiceMockBuilder()
        {
            _reviewAuthorizerService = _mocks.Create<IReviewAuthorizerService>(MockBehavior.Strict);
            _reviewRepository = _mocks.Create<IReviewRepository>(MockBehavior.Strict);
            _uow = _mocks.Create<IUnitOfWork>(MockBehavior.Strict);
        }

        public ReviewServiceMockBuilder WithValidReview(ReviewRequest request)
        {
            _reviewAuthorizerService
                .Setup(x => x.AuthorizeAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .Verifiable();

            _reviewRepository
                .Setup(x => x.Insert(It.IsAny<ReviewEntity>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _uow
                .Setup(x => x.BeginTransaction());

            _uow
                .Setup(x => x.Commit());

            return this;
        }

        public (ReviewService, MockRepository) Build() => (
            new(
                _reviewAuthorizerService.Object,
                _reviewRepository.Object,
                _uow.Object
                ), _mocks);
    }
}
