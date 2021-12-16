using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Core.Entities.ProdutoAggregate;
using Delivery.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateProdutoRequest>
        .WithResponse<CreateProdutoResponse>
    {
        private readonly IRepository<Produto> _repository;
        private readonly IMapper _mapper;

        public Create(IRepository<Produto> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("/produto")]
        [SwaggerOperation(
            Summary = "Cria um novo Produto",
            Description = "Cria um novo Produto",
            OperationId = "Produto.Create",
            Tags = new[] { "ProdutoEndpoints" })
        ]
        public override async Task<ActionResult<CreateProdutoResponse>> HandleAsync(
            [FromBody] CreateProdutoRequest request, CancellationToken cancellationToken = default)
        {
            var response = new CreateProdutoResponse();

            var newProduto = new Produto(request.Nome, request.Descricao, request.Preco, request.QuantidadeEmEstoque);

            var createdItem = await _repository.AddAsync(newProduto, cancellationToken);

            response.Produto = _mapper.Map<ProdutoDTO>(createdItem);

            return Ok(response);
        }
    }
}
