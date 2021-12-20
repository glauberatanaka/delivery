using Delivery.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.Api.Modules.Installer
{
    public class HttpClientInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var cepClientUrl = configuration.GetValue<string>("CepClientUrl");
            services.AddHttpClient("CepClient", client =>
            {
                client.BaseAddress = new Uri(cepClientUrl);
            }); ;
        }
    }
}
