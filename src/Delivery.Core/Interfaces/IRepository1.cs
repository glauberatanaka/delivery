using Delivery.Core.Entities.CarrinhoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface IRepository
    {
        Task<Carrinho> AdicionaItemAoCarrinhoAsync(string identityUserId,
            int produtoId, int quantidade, CancellationToken cancelationToken = default);

        Task RemoveCarrinhoAsync(int carrinhoId);

        Task<Carrinho> DefineQuantidade(string carrinhoId, int produtoId, int quantidade,
            CancellationToken cancellationToken = default);
    }
}
