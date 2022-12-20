using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Reviews.CommandApi.Api.Middlewares;
using Reviews.CommandApi.Core.Validators.Models.Requests;
using Swashbuckle.AspNetCore.Filters;
using System.IO.Compression;
using System.Reflection;
using HttpHeader = Reviews.CommandApi.Api.Constants.HttpHeader;

namespace Reviews.CommandApi.Api.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddApi(this IServiceCollection services) =>
            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddControllers(options =>
                {
                    options.Filters.Add<IncomingMiddleware>();
                    options.Filters.Add<OutgoingMiddleware>();
                })
                .AddValidators()
                .AddControllersAsServices()
                .Services
                .AddMiddlewares()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Review Command API",
                        Description = "",
                        Version = "v1",
                        Contact = new()
                        {
                            Name = "Samuel Squad",
                            Email = "scamilo.dev@gmail.com"
                        },
                    });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                            },
                            Array.Empty<string>()
                        },
                    });

                    var xmlApiFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlApiPath = Path.Combine(AppContext.BaseDirectory, xmlApiFile);

                    c.ExampleFilters();
                    c.OperationFilter<AddResponseHeadersFilter>();
                    c.CustomSchemaIds(type => type.FullName);
                    c.IncludeXmlComments(xmlApiPath);
                })
                .AddSwaggerExamplesFromAssemblyOf<Program>()
                .Configure<GzipCompressionProviderOptions>(gzipCompressOptions => gzipCompressOptions.Level = CompressionLevel.Fastest)
                .AddResponseCompression(options =>
                {
                    options.EnableForHttps = true;
                    options.Providers.Add<GzipCompressionProvider>(); 
                    options.MimeTypes = ResponseCompressionDefaults.MimeTypes
                        .Concat(new[] { HttpHeader.JsonContentType });
                });

        private static IMvcBuilder AddValidators(this IMvcBuilder service) =>
            service
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<ReviewRequestValidator>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

        private static IServiceCollection AddMiddlewares(this IServiceCollection services) =>
            services
                .AddTransient<IValidatorInterceptor, ValidatorMiddleware>()
                .AddScoped<IncomingMiddleware>()
                .AddScoped<OutgoingMiddleware>();
    }
}
