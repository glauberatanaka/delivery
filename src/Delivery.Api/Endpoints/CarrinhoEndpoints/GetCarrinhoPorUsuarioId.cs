using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Api.Dtos;
using Delivery.Core.Entities.CarrinhoAggregate;
using Delivery.Core.Interfaces;
using Delivery.Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.CarrinhoEndpoints
{
    public class GetCarrinhoPorUsuarioId : BaseAsyncEndpoint
        .WithRequest<string>
        .WithResponse<GetCarrinhoPorUsuarioIdResponse>
    {
        private readonly IRepository<Carrinho> _carrinhoRepository;
        private readonly IMapper _mapper;

        public GetCarrinhoPorUsuarioId(IRepository<Carrinho> carrinhoRepository, IMapper mapper)
        {
            _carrinhoRepository = carrinhoRepository;
            _mapper = mapper;
        }

        [HttpGet("/carrinho/{identityUserId}")]
        [SwaggerOperation(
            Summary = "Obtém Carrinho por UsuarioId",
            Description = "Obtém Carrinho por UsuarioId",
            OperationId = "Carrinho.GetCarrinhoPorUsuarioId",
            Tags = new[] { "CarrinhoEndpoints" })
        ]
        public async override Task<ActionResult<GetCarrinhoPorUsuarioIdResponse>> HandleAsync(string identityUserId, CancellationToken cancellationToken = default)
        {
            var response = new GetCarrinhoPorUsuarioIdResponse();

            var carrinhoSpec = new CarrinhoComItensEProdutosSpecification(identityUserId);

            var carrinho = await _carrinhoRepository.GetBySpecAsync(carrinhoSpec, cancellationToken);

            if (carrinho is null)
            {
                return NotFound();
            }

            response.Carrinho = _mapper.Map<CarrinhoDto>(carrinho);

            return Ok(response);
        }
    }
}
