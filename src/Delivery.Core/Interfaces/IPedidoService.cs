using Delivery.Core.Entities.PedidoAggregate;
using Delivery.Shared.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface IPedidoService
    {
        Task<Pedido> AdicionaPedido(string identityUserId, string cep, string numero,
            CancellationToken cancellationToken = default);
        Task<Pedido> ResumoPedido(string identityUserId, string cep, string numero = null,
            CancellationToken cancellationToken = default);
        Task<Pedido> AtualizaStatus(int pedidoId, StatusPedido status, CancellationToken cancellationToken = default);
    }
}
