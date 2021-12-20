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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.PedidoEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Resumo : BaseAsyncEndpoint
        .WithRequest<ResumoRequest>
        .WithResponse<ResumoResponse>
    {
        private readonly IPedidoService _pedidoService;
        private readonly IMapper _mapper;

        public Resumo(IPedidoService pedidoService, IMapper mapper)
        {
            _pedidoService = pedidoService;
            _mapper = mapper;
        }

        [HttpGet("/pedido/resumo")]
        [SwaggerOperation(
            Summary = "Resumo do pedido",
            Description = "Resumo do pedido",
            OperationId = "Pedido.GetResumo",
            Tags = new[] { "PedidoEndpoints" })
        ]
        public async override Task<ActionResult<ResumoResponse>> HandleAsync([FromQuery]ResumoRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(request.Cep))
            {
                return BadRequest();
            }

            var response = new ResumoResponse();

            var identityUserId = User.FindFirstValue("IdentityUserId");

            var pedido = await _pedidoService.ResumoPedido(identityUserId, request.Cep, request.Numero, cancellationToken);

            response.Resumo = _mapper.Map<PedidoDto>(pedido);

            return Ok(response);
        }
    }
}
