using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // ✅ Auto DI Services (class kết thúc bằng "Service")
            services.Scan(scan => scan
                      .FromAssemblies(Assembly.GetExecutingAssembly())
                      .AddClasses(c => c.Where(type => type.Name.EndsWith("Service")))
                          .AsSelf()
                          .WithScopedLifetime()
                  );

            return services;
        }
    }
}
