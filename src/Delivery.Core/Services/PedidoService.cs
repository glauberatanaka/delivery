using Ardalis.GuardClauses;
using Delivery.Core.Entities.CarrinhoAggregate;
using Delivery.Core.Entities.PedidoAggregate;
using Delivery.Core.Extensions;
using Delivery.Core.Interfaces;
using Delivery.Core.Specifications;
using Delivery.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Core.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IRepository<Pedido> _pedidoRepository;
        private readonly IFreteService _freteService;
        private readonly IRepository<Carrinho> _carrinhoRepository;

        public PedidoService(IRepository<Pedido> pedidoRepository,
            IFreteService freteService,
            IRepository<Carrinho> carrinhoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _freteService = freteService;
            _carrinhoRepository = carrinhoRepository;
        }

        public async Task<Pedido> AdicionaPedido(Pedido pedido, CancellationToken cancellationToken = default)
        {
            Guard.Against.CepNuloOuVazio(pedido.Endereco.Cep);
            Guard.Against.InvalidFormat(pedido.Endereco.Cep, nameof(pedido.Endereco.Cep), @"^\d{8}$");
            Guard.Against.NullOrEmpty(pedido.Endereco.Numero, nameof(pedido.Endereco.Numero));

            var valorFrete = await _freteService.CalculaFrete(pedido.Endereco.Cep);
            pedido.SetValorFrete(valorFrete);
            pedido.CalculaValorTotal();

            var result = await _pedidoRepository.AddAsync(pedido, cancellationToken);

            return result;
        }

        public async Task<Pedido> ResumoPedido(string identityUserId, string cep, CancellationToken cancellationToken = default)
        {
            var carrinhoSpec = new CarrinhoComItensEProdutosSpecification(identityUserId);
            var carrinho = await _carrinhoRepository.GetBySpecAsync(carrinhoSpec, cancellationToken);

            var pedidoItens = carrinho
                .Itens
                .Select(i => new PedidoItem(0,
                    i.Produto.Nome,
                    i.Produto.Descricao,
                    i.Produto.Preco,
                    i.Quantidade)
                ).ToList();

            var valorFrete = await _freteService.CalculaFrete(cep);

            var pedido = new Pedido(pedidoItens, new PedidoEndereco(cep), valorFrete);

            return pedido;
        }


        public async Task<Pedido> AtualizaStatus(int pedidoId, StatusPedido status, CancellationToken cancellationToken = default)
        {
            //TODO: Verificar se vai limpar lista de itens ou não (faltando include).
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId, cancellationToken);

            Guard.Against.Null(pedido, nameof(pedido));

            if (pedido.Status == status)
            {
                return pedido;
            }

            pedido.SetStatus(status);

            await _pedidoRepository.UpdateAsync(pedido, cancellationToken);

            return pedido;
        }

    }
}
