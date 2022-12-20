using Microsoft.AspNetCore.Mvc;
using Reviews.CommandApi.Api.Examples.Requests;
using Reviews.CommandApi.Core.Interfaces.Services;
using Reviews.CommandApi.Core.Models.Requests;
using Reviews.CommandApi.Core.Models.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace Reviews.CommandApi.Api.Controllers.V1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ReviewsController : ControllerBase
    {
        public readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
            => _reviewService = reviewService;

        [HttpPost]
        [SwaggerRequestExample(typeof(ReviewRequest), typeof(ReviewRequestExample))]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ReviewResponse), StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> RegisterReview(
            [FromBody] ReviewRequest request,
            CancellationToken cancellationToken = default)
        {
            var reviewResponse = await _reviewService
                .RegisterReview(request, cancellationToken);

            return Created(
                uri: $"{Request.Path}/{request.Id}",
                value: reviewResponse);
        }
    }
}
    