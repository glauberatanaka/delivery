using Ardalis.Specification;
using Delivery.Core.Entities.ProdutoAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Delivery.Core.Specifications
{
    public class ProdutoFilterSpecification : Specification<Produto>
    {
        public ProdutoFilterSpecification(string nome, string descricao)
        {
            var nomeLike = $"%{nome}%";
            var descricaoLike = $"%{descricao}%";

            Query
                .OrderBy(x => x.Nome)
                .Where(p => (string.IsNullOrEmpty(nome) || EF.Functions.Like(p.Nome, nomeLike)) &&
                (string.IsNullOrEmpty(descricao) || EF.Functions.Like(p.Nome, descricaoLike)));
        }
    }
}
