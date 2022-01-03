using Ardalis.Specification;
using Delivery.Core.Entities.ProdutoAggregate;
using System.Linq;

namespace Delivery.Core.Specifications
{
    public class ProdutoQuantidadeEmEstoqueSpecification : Specification<Produto, int>, ISingleResultSpecification
    {
        public ProdutoQuantidadeEmEstoqueSpecification(int produtoId)
        {
            Query
                .Select(x => x.QuantidadeEmEstoque)
                .Where(x => x.Id == produtoId);
        }
    }
}
