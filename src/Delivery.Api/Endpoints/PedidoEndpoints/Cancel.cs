using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Api.Dtos;
using Delivery.Core.Entities.PedidoAggregate;
using Delivery.Core.Interfaces;
using Delivery.Core.Specifications;
using Delivery.Shared.Enums;
using Delivery.Shared.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.PedidoEndpoints
{
    [Authorize(Roles = Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Cancel : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<CancelResponse>
    {
        private readonly IPedidoService _pedidoService;
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IMapper _mapper;

        public Cancel(IPedidoService pedidoService, IRepository<Pedido> pedidoRepository, IMapper mapper)
        {
            _pedidoService = pedidoService;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        [HttpPatch("/pedido/cancelar")]
        [SwaggerOperation(
            Summary = "Cancelar pedido",
            Description = "Cancelar pedido",
            OperationId = "Pedido.Cancel",
            Tags = new[] { "PedidoEndpoints" })
        ]
        public async override Task<ActionResult<CancelResponse>> HandleAsync(int pedidoId, CancellationToken cancellationToken = default)
        {
            var response = new CancelResponse();
            var pedidoSpec = new PedidoComItensPorIdSpecification(pedidoId);
            var pedido = await _pedidoRepository.GetBySpecAsync(pedidoSpec, cancellationToken);

            if (pedido is null)
            {
                return NotFound();
            }

            if (pedido.Status is not StatusPedido.AguardandoPagamento and not StatusPedido.EmProcessamento)
            {
                return BadRequest();
            }

            _ = await _pedidoService.CancelaPedido(pedido, cancellationToken);

            response.Pedido = _mapper.Map<PedidoDto>(pedido);

            return Ok(response);
        }
    }
}
