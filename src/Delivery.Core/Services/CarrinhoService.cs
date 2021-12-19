using Ardalis.GuardClauses;
using Delivery.Core.Entities.CarrinhoAggregate;
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

        public CarrinhoService(IRepository<Carrinho> carrinhoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
        }

        public async Task<Carrinho> AdicionaItemAoCarrinhoAsync(string entityUserId,
            int produtoId, int quantidade, CancellationToken cancelationToken = default)
        {
            var carrinhoSpec = new CarrinhoComItensEProdutosSpecification(entityUserId);
            var carrinho = await _carrinhoRepository.GetBySpecAsync(carrinhoSpec, cancelationToken);

            if (carrinho is null)
            {
                carrinho = new Carrinho(entityUserId);
                await _carrinhoRepository.AddAsync(carrinho, cancelationToken);
            }

            carrinho.AddItem(produtoId, quantidade);

            await _carrinhoRepository.UpdateAsync(carrinho, cancelationToken);
            return carrinho;
        }

        public async Task DeletaCarrinhoAsync(int carrinhoId)
        {
            var carrinho = await _carrinhoRepository.GetByIdAsync(carrinhoId);

            await _carrinhoRepository.DeleteAsync(carrinho);
        }

        public async Task<Carrinho> DefineQuantidade(int carrinhoId, int itemId, int quantidade,
            CancellationToken cancellationToken = default)
        {
            Guard.Against.Null(quantidade, nameof(quantidade));
            var carrinhoSpec = new CarrinhoComItensEProdutosSpecification(carrinhoId);
            var carrinho = await _carrinhoRepository.GetBySpecAsync(carrinhoSpec, cancellationToken);
            Guard.Against.CarrinhoNulo(carrinhoId, carrinho);

            carrinho
                .Items
                .Where(x => x.Id == itemId)
                .FirstOrDefault()
                .SetQuantidade(quantidade);

            carrinho.RemoveItensVazios();

            await _carrinhoRepository.UpdateAsync(carrinho, cancellationToken);
            return carrinho;
        }
    }
}
