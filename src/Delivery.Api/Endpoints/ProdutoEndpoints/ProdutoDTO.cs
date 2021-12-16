namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class ProdutoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
    }
}
