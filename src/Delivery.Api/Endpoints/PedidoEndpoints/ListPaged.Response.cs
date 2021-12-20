using Delivery.Api.Dtos;
using System.Collections.Generic;

namespace Delivery.Api.Endpoints.PedidoEndpoints
{
    public class ListPagedResponse
    {
        public List<PedidoDto> Pedidos { get; set; }
        public int Total { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}