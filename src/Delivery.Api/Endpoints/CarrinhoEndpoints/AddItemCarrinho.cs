using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Api.Dtos;
using Delivery.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.CarrinhoEndpoints
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AddItemCarrinho : BaseAsyncEndpoint
        .WithRequest<AddItemCarrinhoRequest>
        .WithResponse<AddItemCarrinhoResponse>
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly IMapper _mapper;

        public AddItemCarrinho(ICarrinhoService carrinhoService, IMapper mapper)
        {
            _carrinhoService = carrinhoService;
            _mapper = mapper;
        }

        [HttpPost("/carrinho")]
        [SwaggerOperation(
            Summary = "Adicionar item a carrinho",
            Description = "Adicionar item a carrinho",
            OperationId = "Carrinho.AddItemCarrinho",
            Tags = new[] { "CarrinhoEndpoints" })
        ]
        public async override Task<ActionResult<AddItemCarrinhoResponse>> HandleAsync(AddItemCarrinhoRequest request, CancellationToken cancellationToken = default)
        {
            var identityUserId = User.FindFirstValue("IdentityUserId");
            var response = new AddItemCarrinhoResponse();

            var carrinho = await _carrinhoService.AdicionaItemAoCarrinhoAsync(
                identityUserId,
                request.ProdutoId,
                request.Quantidade,
                cancellationToken
            );

            response.Carrinho = _mapper.Map<CarrinhoDto>(carrinho);

            return response;
        }
    }
}
