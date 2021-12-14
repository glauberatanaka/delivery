using AutoMapper;
using Delivery.Api.Endpoints.AuthenticationEndpoints;
using Delivery.Api.Endpoints.IdentityEndpoints;
using Delivery.Api.Endpoints.ProdutoEndpoints;
using Delivery.Core.Entities.ProdutoAggregate;
using Delivery.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Delivery.Api.Mapping
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Produto, CreateProdutoResponse>();

            CreateMap<SignInResult, AuthenticateResponse>()
                .ForMember(r => r.Result, y => y.MapFrom(src => src.Succeeded));

            CreateMap<CreateUserRequest, IdentityUser>();

        }
    }
}
