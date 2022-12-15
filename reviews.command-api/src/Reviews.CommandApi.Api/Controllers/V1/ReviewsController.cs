using Microsoft.AspNetCore.Mvc;
using Reviews.CommandApi.Core.Entities.Requests;
using Reviews.CommandApi.Core.Entities.Responses;
using Reviews.CommandApi.Core.Interfaces.Services;

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
            var operationResult = await _reviewService
                .RegisterReview(request, cancellationToken);

            return Created(
                uri: $"{Request.Path}/{operationResult.Result.Id}", 
                value: operationResult.Result);
        }
    }
}
 