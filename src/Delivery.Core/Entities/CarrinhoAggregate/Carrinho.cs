using Delivery.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.Core.Entities.CarrinhoAggregate
{
    public class Carrinho : BaseEntity, IAggregateRoot
    {
        public string IdentityUserId { get; private set; }
        private readonly List<CarrinhoItem> _itens = new();
        public IEnumerable<CarrinhoItem> Itens => _itens.AsReadOnly();

        public Carrinho(string identityUserId)
        {
            IdentityUserId = identityUserId;
        }

        public void AddItem(int produtoId, int quantidade = 1)
        {
            if (!_itens.Any(i => i.ProdutoId == produtoId))
            {
                _itens.Add(new CarrinhoItem(produtoId, quantidade));
                return;
            }

            var carrinhoItem = _itens.FirstOrDefault(i => i.ProdutoId == produtoId);
            carrinhoItem.AddQuantidade(quantidade);
        }

        public void LimparCarrinho() => _itens.Clear();

        public void RemoveItensVazios() => _itens.RemoveAll(i => i.Quantidade <= 0);
    }
}
