using Delivery.Infrastructure.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Delivery.Infrastructure.Modules
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection InstallServicesFromAssembly(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var installers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => typeof(IInstaller).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList(); ;

            installers.ForEach(installer => installer.InstallServices(services, configuration));

            return services;
        }
    }
}
