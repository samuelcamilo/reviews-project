using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reviews.CommandApi.Core.Extensions;
using Reviews.CommandApi.Infra.Data.Extensions;
using Reviews.CommandApi.Infra.Services.Extensions;

namespace Reviews.CommandApi.IoC.DependencyInjection
{
    public static class DependecyInjectionExtension
    {
        public static IServiceCollection AddIoC(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddCore(configuration)
                .AddInfraData(configuration)
                .AddInfraServices(configuration);
    }
}
