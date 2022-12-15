using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reviews.CommandApi.Core.Interfaces.Data;
using Reviews.CommandApi.Infra.Data.Configurations;
using Reviews.CommandApi.Infra.Data.Repositories;
using System.Data;

namespace Reviews.CommandApi.Infra.Data.Extensions
{
    public static class ServiceExtensions
    {
        private const string ConnectionName = "Default";

        public static IServiceCollection AddInfraData(
            this IServiceCollection services,
            IConfiguration configuration) => 
            services
                .AddDatabaseConfig(configuration)
                .AddScoped<IDbConnection>(provider =>
                {
                    var connectionString = provider.GetRequiredService<DatabaseConfig>().ConnectionString;
                    return new SqlConnection(connectionString);
                })
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IReviewRepository, ReviewRepository>();

        private static IServiceCollection AddDatabaseConfig(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddSingleton(_ =>
            {
                SqlMapper.AddTypeMap(typeof(string), DbType.AnsiString);
                var connectionString = configuration.GetConnectionString(ConnectionName);
                var sqlConnection = new SqlConnectionStringBuilder(connectionString)
                {
                    TrustServerCertificate = true,
                    MultiSubnetFailover = false,
                    MultipleActiveResultSets = true,
                    TransactionBinding = "Implicit Unbind",
                    Enlist = false,
                    MinPoolSize = 100,
                    MaxPoolSize = 800,
                    CommandTimeout = 1,
                };

                return new DatabaseConfig
                {
                    ConnectionString = sqlConnection.ConnectionString,
                };
            });
    }
}
