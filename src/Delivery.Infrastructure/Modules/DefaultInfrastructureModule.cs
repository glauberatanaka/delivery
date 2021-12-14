//using Autofac;
//using Delivery.Core.Interfaces;
//using Delivery.Infrastructure.Data;
//using Delivery.Infrastructure.Identity;
//using Microsoft.AspNetCore.Identity;
//using System.Collections.Generic;
//using System.Reflection;
//using Module = Autofac.Module;

//namespace Delivery.Infrastructure.Modules
//{
//    public class DefaultInfrastructureModule : Module
//    {
//        private readonly bool _isDevelopment = false;
//        private readonly List<Assembly> _assemblies = new List<Assembly>();

//        public DefaultInfrastructureModule(bool isDevelopment)
//        {
//            _isDevelopment = isDevelopment;
//        }

//        protected override void Load(ContainerBuilder builder)
//        {
//            if (_isDevelopment)
//            {
//                RegisterDevelopmentOnlyDependencies(builder);
//            }
//            else
//            {
//                RegisterProductionOnlyDependencies(builder);
//            }
//            RegisterCommonDependencies(builder);
//        }

//        private void RegisterCommonDependencies(ContainerBuilder builder)
//        {
//            builder.RegisterGeneric(typeof(EfRepository<>))
//                .As(typeof(IRepository<>))
//                .As(typeof(IReadRepository<>))
//                .InstancePerLifetimeScope();

//            builder.RegisterType<UserManager<ApplicationUser>>().InstancePerLifetimeScope();
//            builder.RegisterType<SignInManager<ApplicationUser>>().InstancePerLifetimeScope();
//            builder.RegisterType<RoleManager<ApplicationRole>>().InstancePerLifetimeScope();
//            builder.RegisterType<TokenClaimService>().As<ITokenClaimsService>()
//                .InstancePerLifetimeScope();

//            //TODO: Adicionar Mediatr?
//        }

//        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
//        {
//            // TODO: Registrar serviços apenas de development
//        }

//        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
//        {
//            // TODO: Registrar serviços apenas de produção
//        }
//    }
//}
using Delivery.Core.Interfaces;
using Delivery.Infrastructure.Data;
using Delivery.Infrastructure.Identity;
using Delivery.Infrastructure.Installers;
using Microsoft.AspNetCore.Identity;
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