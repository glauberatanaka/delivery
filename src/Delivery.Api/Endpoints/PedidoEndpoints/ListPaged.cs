using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Api.Dtos;
using Delivery.Core.Entities.PedidoAggregate;
using Delivery.Core.Interfaces;
using Delivery.Core.Specifications;
using Delivery.Shared.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.PedidoEndpoints
{
    [Authorize(Roles = Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ListPaged : BaseAsyncEndpoint
        .WithRequest<ListPagedRequest>
        .WithResponse<ListPagedResponse>
    {
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IMapper _mapper;

        public ListPaged(IRepository<Pedido> pedidoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        [HttpGet("/pedido")]
        [SwaggerOperation(
            Summary = "Filtro paginado de pedidos",
            Description = "Filtro paginado de pedidos",
            OperationId = "Pedido.ListPaged",
            Tags = new[] { "PedidoEndpoints" })
        ]
        public async override Task<ActionResult<ListPagedResponse>> HandleAsync([FromQuery] ListPagedRequest request, CancellationToken cancellationToken = default)
        {
            var response = new ListPagedResponse();
            var totalFilterSpec = new PedidosComItensFilterSpecification(request.IdentityUserId, request.Status);
            response.Total = await _pedidoRepository.CountAsync(totalFilterSpec, cancellationToken);


            var pedidoListPagedSpec = new PedidoFilterPagedSpecification(
                skip: request.PageIndex * request.PageSize,
                take: request.PageSize,
                identityUserId: request.IdentityUserId,
                status: request.Status);

            var pedidos = await _pedidoRepository.ListAsync(pedidoListPagedSpec);

            response.Pedidos = _mapper.Map<List<PedidoDto>>(pedidos);

            response.PageCount = request.PageSize switch
            {
                > 0 => int.Parse(Math.Floor((decimal)response.Total / request.PageSize)
                    .ToString()),
                _ => response.Total > 0 ? 1 : 0
            };

            response.CurrentPage = request.PageIndex;

            return response;
        }
    }
}
