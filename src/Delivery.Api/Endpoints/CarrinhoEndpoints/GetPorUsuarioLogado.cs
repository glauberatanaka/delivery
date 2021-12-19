using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Api.Dtos;
using Delivery.Core.Entities.CarrinhoAggregate;
using Delivery.Core.Interfaces;
using Delivery.Core.Specifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.CarrinhoEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GetPorUsuarioLogado : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<GetCarrinhoPorUsuarioIdResponse>
    {
        private readonly IRepository<Carrinho> _carrinhoRepository;
        private readonly IMapper _mapper;

        public GetPorUsuarioLogado(IRepository<Carrinho> carrinhoRepository, IMapper mapper)
        {
            _carrinhoRepository = carrinhoRepository;
            _mapper = mapper;
        }

        [HttpGet("/carrinho")]
        [SwaggerOperation(
            Summary = "Obtém Carrinho por usuário logado",
            Description = "Obtém Carrinho por usuário logado",
            OperationId = "Carrinho.GetPorUsuarioLogado",
            Tags = new[] { "CarrinhoEndpoints" })
        ]
        public async override Task<ActionResult<GetCarrinhoPorUsuarioIdResponse>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var identityUserId = User.FindFirstValue("IdentityUserId");

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
