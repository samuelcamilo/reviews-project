using Flurl.Http.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reviews.CommandApi.Core.Interfaces.RestClients;
using Reviews.CommandApi.Infra.Services.Configurations;
using Reviews.CommandApi.Infra.Services.RestClients;

namespace Reviews.CommandApi.Infra.Services.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddInfraServices(
            this IServiceCollection service, IConfiguration configuration) =>
            service
                .AddRestClientApisConfig(configuration)
                .AddAddRestClients()
                .AddFlurlClient();

        private static IServiceCollection AddRestClientApisConfig(
            this IServiceCollection service, IConfiguration configuration) =>
            service
                .AddSingleton(configuration.GetSection("MovieQueryApi").Get<MovieQueryConfig>());

        private static IServiceCollection AddAddRestClients(this IServiceCollection service) => 
            service
                .AddScoped<IMovieQueryClient, MovieQueryClient>();

        private static IServiceCollection AddFlurlClient(this IServiceCollection service)
        {
            //FlurlHttp.Configure(settings => settings
            //    .JsonSerializer = new JsonApiSettings());

            return service
                .AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();
        }
    }
}
