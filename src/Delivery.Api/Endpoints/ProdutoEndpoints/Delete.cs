using Ardalis.ApiEndpoints;
using Delivery.Core.Entities.ProdutoAggregate;
using Delivery.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class Delete : BaseAsyncEndpoint
        .WithRequest<DeleteRequest>
        .WithResponse<DeleteResponse>
    {
        private readonly IRepository<Produto> _produtoRepository;

        public Delete(IRepository<Produto> produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpDelete("produto/{CatalogItemId}")]
        [SwaggerOperation(
            Summary = "Deleta um Produto",
            Description = "Deleta um Produto",
            OperationId = "Produto.Delete",
            Tags = new[] { "ProdutoEndpoints" })
        ]
        public override async Task<ActionResult<DeleteResponse>> HandleAsync(DeleteRequest request, CancellationToken cancellationToken = default)
        {
            var response = new DeleteResponse();

            var item = await _produtoRepository.GetByIdAsync(request.ProdutoId, cancellationToken);

            if (item is null) { return NotFound(); }

            await _produtoRepository.DeleteAsync(item, cancellationToken);

            return Ok(response);
        }
    }
}
