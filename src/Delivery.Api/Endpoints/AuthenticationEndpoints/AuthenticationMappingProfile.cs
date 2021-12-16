using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Delivery.Api.Endpoints.AuthenticationEndpoints
{
    public class AuthenticationMappingProfile : Profile
    {
        public AuthenticationMappingProfile()
        {
            CreateMap<SignInResult, AuthenticateResponse>()
               .ForMember(r => r.Result, opt => opt.MapFrom(src => src.Succeeded));
        }
    }
}
