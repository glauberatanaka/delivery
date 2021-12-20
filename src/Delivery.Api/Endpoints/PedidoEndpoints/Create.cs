using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Api.Dtos;
using Delivery.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.PedidoEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateRequest>
        .WithResponse<CreateResponse>
    {
        private readonly IPedidoService _pedidoService;
        private readonly IMapper _mapper;

        public Create(IPedidoService pedidoService, IMapper mapper)
        {
            _pedidoService = pedidoService;
            _mapper = mapper;
        }

        [HttpPost("/pedido")]
        [SwaggerOperation(
            Summary = "Adiciona pedido",
            Description = "Adiciona pedido",
            OperationId = "Pedido.Create",
            Tags = new[] { "PedidoEndpoints" })
        ]
        public async override Task<ActionResult<CreateResponse>> HandleAsync(CreateRequest request, CancellationToken cancellationToken = default)
        {
            var response = new CreateResponse();

            var identityUserId = User.FindFirstValue("IdentityUserId");

            var pedido = await _pedidoService.AdicionaPedido(identityUserId, request.Cep, request.Numero, cancellationToken);

            response.Pedido = _mapper.Map<PedidoDto>(pedido);

            return Ok(response);
        }
    }
}
