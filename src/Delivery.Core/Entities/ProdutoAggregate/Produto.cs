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

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }

    }
}
