using Delivery.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Entities.PedidoAggregate
{
    public class Pedido : BaseEntity, IAggregateRoot
    {
        private Pedido()
        {
        }

        public int MyProperty { get; set; }
    }
}
