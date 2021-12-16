using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Delivery.Api.Endpoints.IdentityEndpoints
{
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<CreateUserRequest, IdentityUser>();
        }
    }
}
