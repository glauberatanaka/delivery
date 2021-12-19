namespace Delivery.Api.Endpoints.CarrinhoEndpoints
{
    public class SetQuantidadeRequest
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}