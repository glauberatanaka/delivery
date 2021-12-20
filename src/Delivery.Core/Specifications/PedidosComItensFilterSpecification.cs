using Ardalis.Specification;
using Delivery.Core.Entities.PedidoAggregate;
using Delivery.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Specifications
{
    public class PedidosComItensFilterSpecification : Specification<Pedido>
    {
        public PedidosComItensFilterSpecification(string identityUserId, StatusPedido? status )
        {
            Query
                .Where(p => (string.IsNullOrEmpty(identityUserId) || p.IdentityUserId == identityUserId) &&
                    (!status.HasValue || p.Status == (StatusPedido)status))
                .Include(p => p.Itens)
                .Include(p => p.IdentityUser);

        }
    }
}
