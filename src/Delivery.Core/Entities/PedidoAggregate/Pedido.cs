using Ardalis.GuardClauses;
using Delivery.Core.Interfaces;
using Delivery.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delivery.Core.Entities.PedidoAggregate
{
    public class Pedido : BaseEntity, IAggregateRoot
    {
        private readonly List<PedidoItem> _pedidoItens;
        public IEnumerable<PedidoItem> PedidoItens => _pedidoItens.AsReadOnly();

        public PedidoEndereco Endereco { get; private set; }

        public StatusPedido Status { get; private set; } = StatusPedido.AguardandoPagamento;

        public DateTime DataCadastro { get; private set; } = DateTime.Now;

        public decimal ValorTotal { get; private set; }

        public decimal ValorFrete { get; private set; }


        private Pedido()
        {
        }

        public Pedido(List<PedidoItem> pedidoItens, PedidoEndereco endereco, decimal valorFrete)
        {
            _pedidoItens = pedidoItens;
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
            var valorTotal = _pedidoItens.Sum(i => i.Preco);
            ValorTotal = valorTotal;
        }

        public void SetValorFrete(decimal valorFrete)
        {
            ValorFrete = valorFrete;
        }

        public void AddItem(PedidoItem pedidoItem)
        {
            if (!_pedidoItens.Any(item => item == pedidoItem))
            {
                _pedidoItens.Add(pedidoItem);
                return;
            }

            var item = _pedidoItens.FirstOrDefault(item => item == pedidoItem);
            item.AddQuantidade(pedidoItem.Quantidade);
        }

    }
}
