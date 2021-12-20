using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Api.Dtos;
using Delivery.Infrastructure.Identity;
using Delivery.Shared.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.IdentityEndpoints
{
    [Authorize(Roles = Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ListUsers : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<ListUsersResponse>
    {
        private readonly IIdentityUserInterface _identityUserRepository;
        private readonly IMapper _mapper;

        public ListUsers(IIdentityUserInterface identityUserRepository, IMapper mapper)
        {
            _identityUserRepository = identityUserRepository;
            _mapper = mapper;
        }

        [HttpGet("/usuario")]
        [SwaggerOperation(
            Summary = "Obtém lista de Usuários",
            Description = "Obtém lista de todos Usuários",
            OperationId = "Identity.List",
            Tags = new[] { "IdentityEndpoints" })
        ]
        public async override Task<ActionResult<ListUsersResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var retorno = new ListUsersResponse();

            var users = await _identityUserRepository.ListAsync();

            retorno.Usuarios = _mapper.Map<List<IdentityUserDto>>(users);

            return Ok(retorno);
        }
    }
}
