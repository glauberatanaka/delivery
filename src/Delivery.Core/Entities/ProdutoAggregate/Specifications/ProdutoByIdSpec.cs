using Ardalis.Specification;
using System.Linq;

namespace Delivery.Core.Entities.ProdutoAggregate.Specifications
{
    public class ProdutoByIdSpec : Specification<Produto>, ISingleResultSpecification
    {
        public ProdutoByIdSpec(int produtoId)
        {
            Query
                .Where(p => p.Id == produtoId);
        }
    }
}
