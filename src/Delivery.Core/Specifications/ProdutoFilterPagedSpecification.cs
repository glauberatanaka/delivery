using Ardalis.Specification;
using Delivery.Core.Entities.ProdutoAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Delivery.Core.Specifications
{
    public class ProdutoFilterPagedSpecification : Specification<Produto>
    {
        public ProdutoFilterPagedSpecification(int skip, int take, string nome, string descricao) : base()
        {
            if (take == 0)
            {
                take = int.MaxValue;
            }

            var nomeLike = $"%{nome}%";
            var descricaoLike = $"%{descricao}%";

            Query
                .OrderBy(x => x.Nome)
                .Where(p => (string.IsNullOrEmpty(nome) || EF.Functions.Like(p.Nome, nomeLike)) &&
                (string.IsNullOrEmpty(descricao) || EF.Functions.Like(p.Nome, descricaoLike)))
                .Skip(skip).Take(take);
        }
    }
}
