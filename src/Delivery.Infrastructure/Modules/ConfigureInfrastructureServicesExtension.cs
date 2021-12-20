using Delivery.Core.Interfaces;
using Delivery.Infrastructure.Data;
using Delivery.Infrastructure.Identity;
using Delivery.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Delivery.Infrastructure.Modules
{
    public static class ConfigureInfrastructureServicesExtension
    {
        public static IServiceCollection ConfigureInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped<IIdentityUserInterface, IdentityUserRepository>();
            services.AddScoped<ITokenClaimsService, TokenClaimService>();
            services.AddScoped<ICepRepository, CepRepository>();
            return services;
        }

        public static IServiceCollection InstallServicesFromAssembly(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var installers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => typeof(IInstaller).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));

            return services;
        }
    }
}
