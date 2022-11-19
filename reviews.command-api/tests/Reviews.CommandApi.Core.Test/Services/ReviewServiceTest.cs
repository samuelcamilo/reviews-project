using FluentAssertions;
using NUnit.Framework;
using Reviews.CommandApi.Core.Entities.Requests;
using Reviews.CommandApi.Core.Test.Builders;
using Reviews.CommandApi.Shared;

namespace Reviews.CommandApi.Core.Test.Services
{
    public class ReviewServiceTest
    {
        [Test]
        public async Task RegisterReview_GivenValidRequest_ThenReturnOperationResultAsSuccess()
        {
            // Arrange
            var request = GetValidRequest();
            var (sut, mocks) = new ReviewMockBuilder()
                .WithValidReview(request)
                .Build();

            // Act
            var operationResult = await sut.RegisterReview(request);

            // Assert
            operationResult.IsSuccess.Should().Be(OperationResult.Success());
            mocks.VerifyAll();
        }

        private ReviewRequest GetValidRequest() =>
            new()
            {
                Id = Guid.NewGuid(),
                MovieId = Guid.NewGuid(),
                Title = "So Bad",
                Message = "Was bad experience.",
                CreatedAt = DateTime.Now,
                CreatedBy = "Samuel Camilo"
            };
    }
}
