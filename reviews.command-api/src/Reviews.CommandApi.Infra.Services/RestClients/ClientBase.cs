using Flurl.Http;

namespace Reviews.CommandApi.Infra.Services.RestClients
{
    internal abstract class ClientBase
    {
        protected static IFlurlRequest SetupClient(string url, uint timeoutInMilliseconds, string authorizationToken = null) =>
            url
                .WithOAuthBearerToken(authorizationToken)
                .WithHeader("Content-Type", "application/json")
                .ConfigureRequest(cfg =>
                {
                    cfg.Timeout = timeoutInMilliseconds is 0 ? null : TimeSpan.FromMilliseconds(timeoutInMilliseconds);
                    cfg.UrlEncodedSerializer = null;
                });
    }
}
