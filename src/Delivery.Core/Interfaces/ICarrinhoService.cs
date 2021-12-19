using Delivery.Core.Entities.CarrinhoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface ICarrinhoService
    {
        Task<Carrinho> AdicionaItemAoCarrinhoAsync(string entityUserId,
            int produtoId, int quantidade, CancellationToken cancelationToken = default);

        Task DeletaCarrinhoAsync(int carrinhoId);

        Task<Carrinho> DefineQuantidade(int carrinhoId, int itemId, int quantidade,
            CancellationToken cancellationToken = default);
    }
}
