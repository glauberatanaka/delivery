using Delivery.Infrastructure.Installers;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Api.Modules.Installer
{
    public class FluentValidationInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Startup>());
        }
    }
}
