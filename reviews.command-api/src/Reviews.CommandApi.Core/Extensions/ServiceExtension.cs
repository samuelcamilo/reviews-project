using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reviews.CommandApi.Core.Configurations;
using Reviews.CommandApi.Core.Events;
using Reviews.CommandApi.Core.Events.ReviewRejected;
using Reviews.CommandApi.Core.Interfaces.Events;
using Reviews.CommandApi.Core.Interfaces.Notifications;
using Reviews.CommandApi.Core.Interfaces.Services;
using Reviews.CommandApi.Core.Notifications;
using Reviews.CommandApi.Core.Services;

namespace Reviews.CommandApi.Core.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddCore(
            this IServiceCollection services, IConfiguration configuration) =>
            services.AddConfigs(configuration)
                    .AddServices();

        private static IServiceCollection AddConfigs(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSingleton<ExecutionTimeoutConfig>(_ => new()
            {
                AuthorizationExecutionTimeoutMilliseconds = configuration
                    .GetValue("ExecutionTimeout:AuthorizationExecutionTimeoutMilliseconds", int.MinValue)
            });

        private static IServiceCollection AddServices(this IServiceCollection services) =>
            services.AddScoped<IReviewService, ReviewService>()
                    .AddScoped<IReviewAuthorizerService, ReviewAuthorizerService>()
                    .AddScoped<IEventHandler<ReviewRejectedEvent>, ReviewRejectedEventHandler>()
                    .AddScoped<IEventRaiser, EventRaiser>()
                    .AddScoped<INotification, Notification>();
    }
}
