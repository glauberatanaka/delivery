using Ardalis.GuardClauses;
using Delivery.Core.Entities.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Entities.CarrinhoAggregate
{
    public class CarrinhoItem : BaseEntity
    {
        public int CarrinhoId { get; private set; }
        public int ProdutoId { get; private set; }
        public Produto Produto { get; private set; }
        public int Quantidade { get; private set; }

        public CarrinhoItem(int produtoId, int quantidade)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }

        public CarrinhoItem(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public void AddQuantidade(int quantidade)
        {
            Guard.Against.OutOfRange(quantidade, nameof(quantidade), 0, int.MaxValue);

            Quantidade += quantidade;
        }

        public void SetQuantidade(int quantidade)
        {
            Guard.Against.OutOfRange(quantidade, nameof(quantidade), 0, int.MaxValue);

            Quantidade = quantidade;
        }
    }
}
