namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class CreateProdutoResponse
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public decimal Preco { get; set; }
    }
}
