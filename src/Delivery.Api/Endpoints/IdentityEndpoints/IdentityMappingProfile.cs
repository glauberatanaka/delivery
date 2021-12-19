using AutoMapper;
using Delivery.Api.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Delivery.Api.Endpoints.IdentityEndpoints
{
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<CreateUserRequest, IdentityUser>();

            CreateMap<IdentityUser, IdentityUserDto>()
                .ForMember(dest => dest.IdentityUserId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
