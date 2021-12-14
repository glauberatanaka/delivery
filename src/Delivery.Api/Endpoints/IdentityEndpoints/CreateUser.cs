using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.IdentityEndpoints
{
    public class CreateUser : BaseAsyncEndpoint
        .WithRequest<CreateUserRequest>
        .WithResponse<CreateUserResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;

        public CreateUser(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        [HttpPost("/Usuario")]
        [SwaggerOperation(
            Summary = "Cria um novo Usuário",
            Description = "Cria um novo Usuário",
            OperationId = "User.Create",
            Tags = new[] { "IdentityEndpoints" })
        ]
        public override async Task<ActionResult<CreateUserResponse>> HandleAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null)
            {
                return new CreateUserResponse { 
                    Errors = new List<string> { "Usuário já cadastrado com este e-mail" } 
                };
            }
            var appUser = _mapper.Map<IdentityUser>(request);
            var createdUser = await _userManager.CreateAsync(appUser, request.Password);

            if (!createdUser.Succeeded)
            {
                var errorRes = new CreateUserResponse
                {
                    Errors = createdUser.Errors.Select(e => e.Description).ToList()
                };
                return errorRes;
            }
            //TODO retornar JWT
            return Ok();
        }
    }
}
