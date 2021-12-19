using Delivery.Api.Dtos;
using System.Collections.Generic;

namespace Delivery.Api.Endpoints.IdentityEndpoints
{
    public class ListUsersResponse
    {
        public List<IdentityUserDto> Usuarios { get; set; }
    }
}