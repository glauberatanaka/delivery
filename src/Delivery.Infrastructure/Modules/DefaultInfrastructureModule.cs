using Delivery.Core.Interfaces;
using Delivery.Infrastructure.Data;
using Delivery.Infrastructure.Identity;
using Delivery.Infrastructure.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Infrastructure.Modules
{
    public class DefaultInfrastructureModule : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
            services.AddScoped<ITokenClaimsService, TokenClaimService>();

        }
    }
}