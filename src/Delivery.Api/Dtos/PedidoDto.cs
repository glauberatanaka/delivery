using Delivery.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.Api.Dtos
{
    public class PedidoDto
    {
        public List<PedidoItemDto> Itens { get; private set; }

        public PedidoEnderecoDto Endereco { get; private set; }

        public StatusPedido Status { get; private set; }

        public DateTime? DataCadastro { get; private set; }

        public decimal ValorTotal { get; private set; }

        public decimal ValorFrete { get; private set; }
    }
}
