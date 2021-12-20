using Delivery.Shared.Enums;
using System;
using System.Collections.Generic;

namespace Delivery.Api.Dtos
{
    public class PedidoDto
    {
        public string IdentityUserId { get; set; }
        public string UsuarioNome { get; set; }

        public int PedidoId { get; set; }

        public PedidoEnderecoDto Endereco { get; set; }

        public StatusPedido Status { get; set; }

        public DateTime? DataCadastro { get; set; }

        public decimal ValorTotal { get; set; }

        public decimal ValorFrete { get; set; }

        public List<PedidoItemDto> Itens { get; set; }
    }
}
