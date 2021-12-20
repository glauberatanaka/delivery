using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Core.Interfaces;
using Delivery.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.AuthenticationEndpoints
{
    public class Authenticate : BaseAsyncEndpoint
        .WithRequest<AuthenticateRequest>
        .WithResponse<AuthenticateResponse>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenClaimsService _tokenClaimsService;
        private readonly IMapper _mapper;

        public Authenticate(SignInManager<IdentityUser> signInManager,
            ITokenClaimsService tokenClaimsService,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _tokenClaimsService = tokenClaimsService;
            _mapper = mapper;
        }

        [HttpPost("/auth")]
        [SwaggerOperation(
            Summary = "Autentica usuário",
            Description = "{\"username\": \"user@user.com\", \"password\": \"User@123\"}<br />" +
            "{\"username\": \"admin@admin.com\", \"password\": \"Admin@123\"}<br />" +
            "Adicionar \"bearer \" antes do token",
            OperationId = "auth.authenticate",
            Tags = new[] { "AuthEndpoints" })
        ]
        public override async Task<ActionResult<AuthenticateResponse>> HandleAsync(AuthenticateRequest request, CancellationToken cancellationToken = default)
        {
            //var response = new AuthenticateResponse();

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            //var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            //response.Result = result.Succeeded;
            //response.IsLockedOut = result.IsLockedOut;
            //response.IsNotAllowed = result.IsNotAllowed;
            //response.RequiresTwoFactor = result.RequiresTwoFactor;
            //response.Username = request.Username;

            var response = _mapper.Map<AuthenticateResponse>(result);

            if (result.Succeeded)
            {
                response.Token = await _tokenClaimsService.GetTokenAsync(request.Username);
            }

            return response;
        }
    }
}
