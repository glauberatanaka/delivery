using Delivery.Api.Dtos;
using System.Collections.Generic;

namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class ListProdutosResponse
    {
        public List<ProdutoDto> Produtos { get; set; } = new List<ProdutoDto>();
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
    }
}
