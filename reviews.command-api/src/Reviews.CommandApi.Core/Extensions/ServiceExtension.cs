using Microsoft.Extensions.DependencyInjection;
using Reviews.CommandApi.Core.Interfaces.Services;
using Reviews.CommandApi.Core.Services;

namespace Reviews.CommandApi.Core.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddCore(this IServiceCollection services) 
            => services.AddScoped<IReviewService, ReviewService>()
                       .AddScoped<IReviewAuthorizerService, ReviewAuthorizerService>();
    }
}
