using Reviews.CommandApi.Core.Configurations;
using Reviews.CommandApi.Core.Constants;
using Reviews.CommandApi.Core.Events.ReviewRejected;
using Reviews.CommandApi.Core.Interfaces.Events;
using Reviews.CommandApi.Core.Interfaces.RestClients;
using Reviews.CommandApi.Core.Interfaces.Services;
using Reviews.CommandApi.Core.Models.Requests;

namespace Reviews.CommandApi.Core.Services
{
    public class ReviewAuthorizerService : IReviewAuthorizerService
    {
        private readonly IMovieQueryClient _movieQueryClient;
        private readonly IEventRaiser _eventRaiser;

        private CancellationTokenSource _cts;
        private readonly int _authorizationExecutionTimeoutMilliseconds;

        public ReviewAuthorizerService(IMovieQueryClient movieQueryClient, IEventRaiser eventRaiser, ExecutionTimeoutConfig executionTimeoutConfig) =>
            (_movieQueryClient, _eventRaiser, _authorizationExecutionTimeoutMilliseconds) = (movieQueryClient, eventRaiser, executionTimeoutConfig.AuthorizationExecutionTimeoutMilliseconds);

        public async Task<bool> AuthorizeAsync(ReviewRequest request, CancellationToken cancellationToken)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _cts.CancelAfter(millisecondsDelay: _authorizationExecutionTimeoutMilliseconds);

            try
            {
                var authorizedMovie = await 
                    MovieExistsAsync(request: request, cancellationToken: cancellationToken);

                if (!authorizedMovie)
                    return false;
                
                return true;
            }
            catch(OperationCanceledException opex)
            {
                _cts.Cancel();
                return false;
            }
            catch(Exception exception)
            {
                _cts.Cancel();
                return false;
            }
            finally
            {
                _cts.Dispose();
            }
        }

        private async Task<bool> MovieExistsAsync(ReviewRequest request, CancellationToken cancellationToken)
        {
            var existsMovie = await _movieQueryClient
                .GetAsync(movieId: request.MovieId, cancellationToken: cancellationToken);

            if (!existsMovie)
            {
                _cts.Cancel();
                await _eventRaiser.RaiseAsync(new ReviewRejectedEvent(
                    request: request, 
                    responseCode: ResponseCodes.NonExistentMovie));

                return false;
            }

            return true;
        }
    }
}
