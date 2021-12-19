using System.Collections.Generic;

namespace Delivery.Api.Dtos
{
    public class CarrinhoDto
    {
        public string IdentityUserId { get; set; }
        public List<CarrinhoItemDto> Itens { get; set; }
    }
}