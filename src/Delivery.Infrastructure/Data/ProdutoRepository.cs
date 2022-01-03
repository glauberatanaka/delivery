using Ardalis.Specification;
using Delivery.Core.Entities.ProdutoAggregate;
using Delivery.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Data
{
    public class ProdutoRepository : EfRepository<Produto>, IProdutoRepository
    {
        private readonly AppDbContext _db;

        public ProdutoRepository(AppDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public async Task<int> ObterQuantidadeEmEstoque(ISpecification<Produto, int> spec, CancellationToken cancellationToken)
        {
            var result = await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
            return result;
        }

        public void UpdateRange(IEnumerable<Produto> produtoListUpdate)
        {
            _db.UpdateRange(produtoListUpdate);
        }
    }
}
