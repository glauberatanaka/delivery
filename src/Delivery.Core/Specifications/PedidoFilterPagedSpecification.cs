using Ardalis.Specification;
using Delivery.Core.Entities.PedidoAggregate;
using Delivery.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Delivery.Core.Specifications
{
    public class PedidoFilterPagedSpecification : Specification<Pedido>
    {
        public PedidoFilterPagedSpecification(int skip, int take, string identityUserId, StatusPedido? status = null) : base()
        {
            if (take == 0)
            {
                take = int.MaxValue;
            }

            var userIdLike = $"%{identityUserId}%";

            Query
                .OrderByDescending(x => x.DataCadastro)
                .Where(p => (string.IsNullOrEmpty(identityUserId) || EF.Functions.Like(p.IdentityUserId, userIdLike)) &&
                    (!status.HasValue || p.Status == (StatusPedido)status))
                .Include(p => p.Itens)
                .Include(p => p.IdentityUser)
                .Skip(skip).Take(take);
        }
    }
}
