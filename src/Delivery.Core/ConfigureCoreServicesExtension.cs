using Delivery.Core.Interfaces;
using Delivery.Core.Services;
using Delivery.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Core
{
    public static class ConfigureCoreServicesExtension
    {
        public static IServiceCollection ConfigureCoreServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IRepository, CarrinhoService>();
            return services;
        }
    }
}