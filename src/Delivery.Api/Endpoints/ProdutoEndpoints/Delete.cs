using Ardalis.ApiEndpoints;
using Delivery.Core.Entities.ProdutoAggregate;
using Delivery.Core.Interfaces;
using Delivery.Shared.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    [Authorize(Roles = Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            Summary = "Deleta um Produto (ADM)",
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
