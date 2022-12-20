using FluentAssertions;
using NUnit.Framework;
using Reviews.CommandApi.Core.Models.Requests;
using Reviews.CommandApi.Core.Test.Builders;

namespace Reviews.CommandApi.Core.Test.Services
{
    public class ReviewServiceTest
    {
        [Test]
        public async Task RegisterReview_GivenValidRequest_ThenReturnOperationResultAsSuccess()
        {
            // Arrange
            var request = GetValidRequest();
            var (sut, mocks) = new ReviewServiceMockBuilder()
                .WithValidReview(request)
                .Build();

            // Act
            var result = await sut.RegisterReview(request);

            // Assert
            result.Should().Be(true);
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
