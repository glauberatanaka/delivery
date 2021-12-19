namespace Delivery.Api.Endpoints.CarrinhoEndpoints
{
    public class AddItemCarrinhoRequest
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}