using Delivery.Core.Entities.CarrinhoAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface ICarrinhoService
    {
        Task<Carrinho> AdicionaItemAoCarrinhoAsync(string identityUserId,
            int produtoId, int quantidade, CancellationToken cancelationToken = default);

        Task RemoveCarrinhoAsync(int carrinhoId);

        Task RemoveCarrinhoAsync(Carrinho carrinho);

        Task<Carrinho> DefineQuantidade(string carrinhoId, int produtoId, int quantidade,
            CancellationToken cancellationToken = default);

        Task<Carrinho> ObterPorIdentityUserId(string identityUserId, CancellationToken cancellationToken = default);
    }
}
