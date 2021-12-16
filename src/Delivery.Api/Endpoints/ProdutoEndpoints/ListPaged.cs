using Ardalis.ApiEndpoints;
using AutoMapper;
using Delivery.Core.Entities.ProdutoAggregate;
using Delivery.Core.Interfaces;
using Delivery.Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class ListPaged : BaseAsyncEndpoint
        .WithRequest<ListPagedRequest>
        .WithResponse<ListProdutosResponse>
    {
        private readonly IRepository<Produto> _repository;
        private readonly IMapper _mapper;

        public ListPaged(IRepository<Produto> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("/produto")]
        [SwaggerOperation(
            Summary = "Obtém lista paginada de Produtos",
            Description = "Obtém lista paginada de todos Produto",
            OperationId = "Produto.List",
            Tags = new[] { "ProdutoEndpoints" })
        ]
        public override async Task<ActionResult<ListProdutosResponse>> HandleAsync([FromQuery] ListPagedRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = new ListProdutosResponse();

            var totalItemsSpec = new ProdutoFilterSpecification(request.Nome, request.Descricao);
            int totalItems = await _repository.CountAsync(totalItemsSpec, cancellationToken);
            response.TotalItems = totalItems;

            var pagedSpec = new ProdutoFilterPagedSpecification(
                skip: request.PageIndex * request.PageSize,
                take: request.PageSize,
                nome: request.Nome,
                descricao: request.Descricao);

            var produtoList = await _repository.ListAsync(pagedSpec, cancellationToken);

            response.Produtos = _mapper.Map<List<ProdutoDTO>>(produtoList);

            response.PageCount = request.PageSize switch
            {
                > 0 => int.Parse(Math.Floor((decimal)totalItems / request.PageSize)
                    .ToString()),
                _ => totalItems > 0 ? 1 : 0
            };

            response.CurrentPage = request.PageIndex;

            return Ok(response);

        }
    }
}
