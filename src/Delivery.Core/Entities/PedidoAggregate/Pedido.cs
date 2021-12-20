using Delivery.Core.Interfaces;
using Delivery.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.Core.Entities.PedidoAggregate
{
    public class Pedido : BaseEntity, IAggregateRoot
    {
        public string IdentityUserId { get; private set; }
        public IdentityUser IdentityUser { get; private set; }

        private readonly List<PedidoItem> _itens;
        public IEnumerable<PedidoItem> Itens => _itens.AsReadOnly();

        public PedidoEndereco Endereco { get; private set; }

        public StatusPedido Status { get; private set; } = StatusPedido.AguardandoPagamento;

        public DateTime DataCadastro { get; private set; } = DateTime.Now;

        public decimal ValorTotal { get; private set; }

        public decimal ValorFrete { get; private set; }


        private Pedido()
        {
        }

        public Pedido(string identityUserId, List<PedidoItem> itens, PedidoEndereco endereco, decimal valorFrete)
        {
            IdentityUserId = identityUserId;
            _itens = itens;
            Endereco = endereco;
            ValorFrete = valorFrete;
        }


        public void SetEndereco(PedidoEndereco endereco)
        {
            Endereco = endereco;
        }

        public void SetStatus(StatusPedido status)
        {
            Status = status;
        }

        public void CalculaValorTotal()
        {
            var valorTotal = _itens.Sum(i => i.Preco * i.Quantidade);
            ValorTotal = valorTotal;
        }

        public void SetValorFrete(decimal valorFrete)
        {
            ValorFrete = valorFrete;
        }

        public void AddItem(PedidoItem pedidoItem)
        {
            if (!_itens.Any(item => item == pedidoItem))
            {
                _itens.Add(pedidoItem);
                return;
            }

            var item = _itens.FirstOrDefault(item => item == pedidoItem);
            item.AddQuantidade(pedidoItem.Quantidade);
        }

    }
}
