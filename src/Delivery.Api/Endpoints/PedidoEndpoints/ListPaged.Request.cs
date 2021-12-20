using Delivery.Shared.Enums;

namespace Delivery.Api.Endpoints.PedidoEndpoints
{
    public class ListPagedRequest
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public string IdentityUserId { get; set; }
        public StatusPedido? Status { get; set; }
    }
}