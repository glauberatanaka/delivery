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
        public Produto(string nome, string descricao, decimal preco)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Preco = preco;
        }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
