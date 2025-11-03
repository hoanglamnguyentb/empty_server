using Application.Interfaces;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;



namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // ✅ Đăng ký generic repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // ✅ Auto DI Repository
            services.Scan(scan => scan
                .FromAssemblyOf<Repository<object>>() // assembly Infrastructure
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );

            return services;
        }
    }
}
