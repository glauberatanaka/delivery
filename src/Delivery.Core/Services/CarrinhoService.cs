using Ardalis.GuardClauses;
using Delivery.Core.Entities.CarrinhoAggregate;
using Delivery.Core.Entities.ProdutoAggregate;
using Delivery.Core.Extensions;
using Delivery.Core.Interfaces;
using Delivery.Core.Specifications;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Core.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly IRepository<Carrinho> _carrinhoRepository;
        private readonly IRepository<Produto> _produtoRepository;

        public CarrinhoService(IRepository<Carrinho> carrinhoRepository, IRepository<Produto> produtoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<Carrinho> AdicionaItemAoCarrinhoAsync(string identityUserId,
            int produtoId, int quantidade, CancellationToken cancelationToken = default)
        {
            var carrinhoSpec = new CarrinhoComItensEProdutosSpecification(identityUserId);
            var carrinho = await _carrinhoRepository.GetBySpecAsync(carrinhoSpec, cancelationToken);

            if (carrinho is null)
            {
                carrinho = new Carrinho(identityUserId);
                await _carrinhoRepository.AddAsync(carrinho, cancelationToken);
            }

            var produto = await _produtoRepository.GetByIdAsync(produtoId, cancelationToken);

            carrinho.AddItem(produto, quantidade);

            await _carrinhoRepository.UpdateAsync(carrinho, cancelationToken);
            return carrinho;
        }

        public async Task RemoveCarrinhoAsync(int carrinhoId)
        {
            var carrinho = await _carrinhoRepository.GetByIdAsync(carrinhoId);

            await _carrinhoRepository.DeleteAsync(carrinho);
        }

        public async Task RemoveCarrinhoAsync(Carrinho carrinho)
        {
            await _carrinhoRepository.DeleteAsync(carrinho);
        }

        public async Task<Carrinho> DefineQuantidade(string identityUserId, int produtoId, int quantidade,
            CancellationToken cancellationToken = default)
        {
            Guard.Against.Null(quantidade, nameof(quantidade));
            var carrinhoSpec = new CarrinhoComItensEProdutosSpecification(identityUserId);
            var carrinho = await _carrinhoRepository.GetBySpecAsync(carrinhoSpec, cancellationToken);
            Guard.Against.CarrinhoNulo(identityUserId, carrinho);

            carrinho
                .Itens
                .Where(x => x.Produto.Id == produtoId)
                .FirstOrDefault()
                .SetQuantidade(quantidade);

            carrinho.RemoveItensVazios();

            await _carrinhoRepository.UpdateAsync(carrinho, cancellationToken);
            return carrinho;
        }

        public async Task<Carrinho> ObterPorIdentityUserId(string identityUserId, CancellationToken cancellationToken = default)
        {
            var carrinhoSpec = new CarrinhoComItensEProdutosSpecification(identityUserId);
            var carrinho = await _carrinhoRepository.GetBySpecAsync(carrinhoSpec, cancellationToken);

            return carrinho;
        }
    }
}
