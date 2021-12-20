using Ardalis.Specification;
using Delivery.Core.Entities.PedidoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Specifications
{
    public class PedidoComItensPorIdSpecification : Specification<Pedido>, ISingleResultSpecification
    {
        public PedidoComItensPorIdSpecification(int pedidoId)
        {
            Query
                .Where(p => p.Id == pedidoId)
                .Include(p => p.Itens)
                .Include(p => p.IdentityUser);

        }
    }
}
