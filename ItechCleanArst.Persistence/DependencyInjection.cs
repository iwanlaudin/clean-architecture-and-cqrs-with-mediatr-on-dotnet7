using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ItechCleanArst.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, string connectionName = "DefaultConnection")
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(connectionName), npgsqlOption =>
                {
                    npgsqlOption.CommandTimeout(30);
                    npgsqlOption.EnableRetryOnFailure(3);
                });
            });

            return services;
        }
    }
}