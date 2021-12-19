using Ardalis.Specification;
using Delivery.Core.Entities.CarrinhoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Specifications
{
    public class CarrinhoComItensEProdutosSpecification : Specification<Carrinho>, ISingleResultSpecification
    {
        public CarrinhoComItensEProdutosSpecification(string identityUserId)
        {
            Query
                .Where(c => c.IdentityUserId == identityUserId)
                .Include(c => c.Itens)
                    .ThenInclude(i => i.Produto);
        }
        public CarrinhoComItensEProdutosSpecification(int carrinhoId)
        {
            Query
                .Where(c => c.Id == carrinhoId)
                .Include(c => c.Itens)
                    .ThenInclude(i => i.Produto);
        }
    }
}
