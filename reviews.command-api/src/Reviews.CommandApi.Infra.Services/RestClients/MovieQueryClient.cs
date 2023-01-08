using Flurl.Http;
using Reviews.CommandApi.Core.Interfaces.RestClients;
using Reviews.CommandApi.Infra.Services.Configurations;
using Microsoft.AspNetCore.Http;

namespace Reviews.CommandApi.Infra.Services.RestClients
{
    internal class MovieQueryClient : ClientBase, IMovieQueryClient
    {
        private readonly IFlurlRequest _flurl;
        private readonly MovieQueryConfig _config;

        public MovieQueryClient(MovieQueryConfig config) =>
            (_flurl, _config) = (SetupClient(config.BaseUrl, config.TimeoutInMilliseconds), config);

        public async Task<bool> GetAsync(Guid movieId, CancellationToken cancellationToken)
        {
            try
            {
                var query = string.Format(_config.ResourceRoute, movieId);
                //var response = await _flurl
                //    .WithHeader("correlationId", "")
                //    .SetQueryParams(query)
                //    .GetJsonAsync<MovieResponse>(cancellationToken);

                //if (response == default)
                //    return false;

                return true;
            }
            catch(FlurlHttpException flurlException) when (flurlException.StatusCode == StatusCodes.Status404NotFound)
            { 
                return false;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
