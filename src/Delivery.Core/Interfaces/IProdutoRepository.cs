using Ardalis.Specification;
using Delivery.Core.Entities.ProdutoAggregate;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<int> ObterQuantidadeEmEstoque(ISpecification<Produto, int> spec, CancellationToken cancellationToken);
        void UpdateRange(IEnumerable<Produto> produtoListUpdate);
    }
}
