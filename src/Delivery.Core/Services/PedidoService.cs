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
        private readonly ICepRepository _cepRepository;

        public PedidoService(IRepository<Pedido> pedidoRepository,
            IFreteService freteService,
            IRepository<Carrinho> carrinhoRepository,
            ICepRepository cepRepository)
        {
            _pedidoRepository = pedidoRepository;
            _freteService = freteService;
            _carrinhoRepository = carrinhoRepository;
            _cepRepository = cepRepository;
        }

        public async Task<Pedido> AdicionaPedido(string identityUserId, string cep, string numero,
            CancellationToken cancellationToken = default)
        {
            Guard.Against.NullOrEmpty(identityUserId, nameof(identityUserId));
            Guard.Against.CepNuloOuVazio(cep);
            Guard.Against.InvalidFormat(cep, nameof(cep), @"^\d{8}$");
            Guard.Against.NullOrEmpty(numero, nameof(numero));

            var pedido = await ObterPedidoPorUsuarioECep(identityUserId, cep, numero, cancellationToken);

            var result = await _pedidoRepository.AddAsync(pedido, cancellationToken);

            return result;
        }

        public async Task<Pedido> ResumoPedido(string identityUserId, string cep, string numero = null,
            CancellationToken cancellationToken = default)
        {
            Guard.Against.NullOrEmpty(identityUserId, nameof(identityUserId));
            Guard.Against.CepNuloOuVazio(cep);
            Guard.Against.InvalidFormat(cep, nameof(cep), @"^\d{8}$");

            Pedido pedido = await ObterPedidoPorUsuarioECep(identityUserId, cep, numero, cancellationToken);

            return pedido;
        }

        private async Task<Pedido> ObterPedidoPorUsuarioECep(string identityUserId, string cep, string numero = null, CancellationToken cancellationToken = default)
        {
            var carrinhoSpec = new CarrinhoComItensEProdutosSpecification(identityUserId);
            var carrinho = await _carrinhoRepository.GetBySpecAsync(carrinhoSpec, cancellationToken);

            var itens = carrinho
                .Itens
                .Select(i => new PedidoItem(0,
                    i.Produto.Nome,
                    i.Produto.Descricao,
                    i.Produto.Preco,
                    i.Quantidade)
                ).ToList();

            var enderecoCep = await _cepRepository.GetPorCepAsync(cep, cancellationToken);

            var pedidoEndereco = new PedidoEndereco(
                enderecoCep.Cep,
                enderecoCep.Uf,
                enderecoCep.Localidade,
                enderecoCep.Logradouro,
                numero,
                enderecoCep.Complemento
            );

            var valorFrete = _freteService.CalculaFrete(pedidoEndereco.Localidade, pedidoEndereco.Uf);

            var pedido = new Pedido(identityUserId, itens, pedidoEndereco, valorFrete);

            pedido.CalculaValorTotal();

            return pedido;
        }

        public async Task<Pedido> AtualizaStatus(int pedidoId, StatusPedido status, 
            CancellationToken cancellationToken = default)
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
