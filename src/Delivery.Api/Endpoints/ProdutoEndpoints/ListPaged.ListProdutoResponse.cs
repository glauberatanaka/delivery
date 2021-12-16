using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class ListProdutosResponse
    {
        public List<ProdutoDTO> Produtos { get; set; } = new List<ProdutoDTO>();
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
    }
}
