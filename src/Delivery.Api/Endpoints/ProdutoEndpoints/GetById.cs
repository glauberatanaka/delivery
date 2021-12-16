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
    public class GetById : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<GetByIdProdutoResponse>
    {
        private readonly IRepository<Produto> _produtoRepository;
        private readonly IMapper _mapper;

        public GetById(IRepository<Produto> repository, IMapper mapper)
        {
            _produtoRepository = repository;
            _mapper = mapper;
        }
        [HttpGet("/produto/{id}")]
        [SwaggerOperation(
            Summary = "Obtém Produto por Id",
            Description = "Obtém Produto por Id",
            OperationId = "Produto.GetById",
            Tags = new[] { "ProdutoEndpoints" })
        ]
        public override async Task<ActionResult<GetByIdProdutoResponse>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = new GetByIdProdutoResponse();

            var produto = await _produtoRepository.GetByIdAsync(id);

            if (produto is null) { return NotFound(); }

            response.Produto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(response);
        }
    }
}
