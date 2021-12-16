namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class ListPagedRequest
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
