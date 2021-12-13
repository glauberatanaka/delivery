namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class CreateProdutoRequest
    {
        public const string Route = "/Projects";

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
