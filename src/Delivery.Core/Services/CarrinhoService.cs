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
        private readonly IProdutoRepository _produtoRepository;

        public CarrinhoService(IRepository<Carrinho> carrinhoRepository, IProdutoRepository produtoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<Carrinho> AdicionaItemAoCarrinhoAsync(string identityUserId,
            int produtoId, int quantidadeAdicionar, CancellationToken cancelationToken = default)
        {
            var carrinhoSpec = new CarrinhoComItensEProdutosSpecification(identityUserId);
            var carrinho = await _carrinhoRepository.GetBySpecAsync(carrinhoSpec, cancelationToken);
            var produto = await _produtoRepository.GetByIdAsync(produtoId, cancelationToken);

            Guard.Against.Null(produto, nameof(produto));
            var quantidadeVerificar = quantidadeAdicionar;
            if (carrinho is null)
            {
                carrinho = new Carrinho(identityUserId);
                await _carrinhoRepository.AddAsync(carrinho, cancelationToken);
            }
            else if (carrinho.Itens.Any(i => i.ProdutoId == produtoId))
            {
                quantidadeVerificar += carrinho.Itens.Where(i => i.ProdutoId == produtoId).FirstOrDefault().Quantidade;
            }

            Guard.Against.ProdutoForaDeEstoque(produto.QuantidadeEmEstoque, quantidadeVerificar);
            carrinho.AddItem(produto, quantidadeAdicionar);

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

        private async Task<int> ObterQuantidadeEmEstoque(int produtoId, CancellationToken cancellationToken)
        {
            var produtoQuantidadeEstoqueSpec = new ProdutoQuantidadeEmEstoqueSpecification(produtoId);
            var quantidadeEmEstoque = await _produtoRepository
                .ObterQuantidadeEmEstoque(produtoQuantidadeEstoqueSpec, cancellationToken);

            return quantidadeEmEstoque;
        }
    }
}
