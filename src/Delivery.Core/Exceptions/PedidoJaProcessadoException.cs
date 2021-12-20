using Delivery.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Exceptions
{
    public class PedidoJaProcessadoException : Exception
    {
        public PedidoJaProcessadoException() : base("Pedido precisa estar Em Processamento para ser cancelado.")
        {

        }
    }
}
