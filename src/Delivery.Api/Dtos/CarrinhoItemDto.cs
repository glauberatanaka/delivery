using Delivery.Api.Endpoints.ProdutoEndpoints;

namespace Delivery.Api.Dtos
{
    public class CarrinhoItemDto
    {
        public int CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public ProdutoDto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}