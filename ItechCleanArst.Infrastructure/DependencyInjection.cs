using ItechCleanArst.Application.Interfaces;
using ItechCleanArst.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace ItechCleanArst.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // services.AddScoped<IJwtProvider>(provider => provider.GetRequiredService<JwtProvider>());
            services.AddScoped<IJwtProvider, JwtProvider>();

            return services;
        }
    }
}