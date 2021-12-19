using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Api.Dtos;
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
    public class Patch : BaseAsyncEndpoint
        .WithRequest<PatchRequest>
        .WithResponse<PatchResponse>
    {
        private readonly IRepository<Produto> _produtoRepository;
        private readonly IMapper _mapper;

        public Patch(IRepository<Produto> repository, IMapper mapper)
        {
            _produtoRepository = repository;
            _mapper = mapper;
        }
        [HttpPatch("/produto/{id}")]
        [SwaggerOperation(
            Summary = "Atualiza Produto",
            Description = "Atualiza Produto",
            OperationId = "Produto.Patch",
            Tags = new[] { "ProdutoEndpoints" })
        ]
        public override async Task<ActionResult<PatchResponse>> HandleAsync([FromBody]PatchRequest request, CancellationToken cancellationToken = default)
        {
            var response = new PatchResponse();

            var produtoToDelete = await _produtoRepository.GetByIdAsync(request.ProdutoId, cancellationToken);

            if (produtoToDelete is null) { return NotFound(); }

            var produto = _mapper.Map(request, produtoToDelete);

            await _produtoRepository.UpdateAsync(produto, cancellationToken);

            response.Produto = _mapper.Map<ProdutoDto>(produto);

            return Ok(response);
        }
    }
}
