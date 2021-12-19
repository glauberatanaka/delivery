using Delivery.Api.Endpoints.ProdutoEndpoints;

namespace Delivery.Api.Dtos
{
    public class CarrinhoItemDto
    {
        public int CarrinhoItemId { get; set; }
        public ProdutoDto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}