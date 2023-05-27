using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ItechCleanArst.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(action => action.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}