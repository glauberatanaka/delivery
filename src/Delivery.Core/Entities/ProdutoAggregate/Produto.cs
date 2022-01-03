using Ardalis.GuardClauses;
using Delivery.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Entities.ProdutoAggregate
{
    public class Produto : BaseEntity, IAggregateRoot
    {
        private Produto()
        {
        }

        public Produto(string nome, string descricao, decimal preco, int quantidadeEmEstoque)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
            this.QuantidadeEmEstoque = quantidadeEmEstoque;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public int QuantidadeEmEstoque { get; private set; }

        public void SetQuantidadeEmEstoque(int novaQuantidade)
        {
            this.QuantidadeEmEstoque = novaQuantidade;
        }

    }
}
