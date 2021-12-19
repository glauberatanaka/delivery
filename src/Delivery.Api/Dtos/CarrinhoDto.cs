using System.Collections.Generic;

namespace Delivery.Api.Dtos
{
    public class CarrinhoDto
    {
        public string IdentityUserId { get; set; }
        public int CarrinhoId { get; set; }
        public List<CarrinhoItemDto> Itens { get; set; }
    }
}