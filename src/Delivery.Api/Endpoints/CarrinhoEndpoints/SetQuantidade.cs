using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Api.Dtos;
using Delivery.Core.Interfaces;
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
    public class SetQuantidade : BaseAsyncEndpoint
        .WithRequest<SetQuantidadeRequest>
        .WithResponse<SetQuantidadeResponse>
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly IMapper _mapper;

        public SetQuantidade(ICarrinhoService carrinhoService, IMapper mapper)
        {
            _carrinhoService = carrinhoService;
            _mapper = mapper;
        }

        [HttpPatch("/carrinho")]
        [SwaggerOperation(
            Summary = "Altera quantidade de item no carrinho",
            Description = "Altera quantidade de item no carrinho",
            OperationId = "Carrinho.DefineQuantidade",
            Tags = new[] { "CarrinhoEndpoints" })
        ]
        public async override Task<ActionResult<SetQuantidadeResponse>> HandleAsync(SetQuantidadeRequest request, CancellationToken cancellationToken = default)
        {
            var identityUserId = User.FindFirstValue("IdentityUserId");

            var response = new SetQuantidadeResponse();

            var carrinho = await _carrinhoService.DefineQuantidade(
                identityUserId,
                request.ProdutoId,
                request.Quantidade,
                cancellationToken);

            response.Carrinho = _mapper.Map<CarrinhoDto>(carrinho);

            return Ok(response);
        }
    }
}
