using Delivery.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.Core.Entities.CarrinhoAggregate
{
    public class Carrinho : BaseEntity, IAggregateRoot
    {
        public string IdentityUserId { get; private set; }
        private readonly List<CarrinhoItem> _items = new();
        public IEnumerable<CarrinhoItem> Items => _items.AsReadOnly();

        public Carrinho(string identityUserId)
        {
            IdentityUserId = identityUserId;
        }

        public void AddItem(int produtoId, int quantidade = 1)
        {
            if (!_items.Any(i => i.ProdutoId == produtoId))
            {
                _items.Add(new CarrinhoItem(produtoId, quantidade));
                return;
            }

            var carrinhoItem = _items.FirstOrDefault(i => i.ProdutoId == produtoId);
            carrinhoItem.AddQuantidade(quantidade);
        }

        public void LimparCarrinho() => _items.Clear();

        public void RemoveItensVazios() => _items.RemoveAll(i => i.Quantidade <= 0);
    }
}
