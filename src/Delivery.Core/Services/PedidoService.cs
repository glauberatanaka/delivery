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
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICepRepository _cepRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PedidoService(IRepository<Pedido> pedidoRepository,
            IFreteService freteService,
            ICarrinhoService carrinhoService,
            ICepRepository cepRepository,
            IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _freteService = freteService;
            _carrinhoService = carrinhoService;
            _cepRepository = cepRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<Pedido> AdicionaPedido(string identityUserId, string cep, string numero,
            CancellationToken cancellationToken = default)
        {
            Guard.Against.NullOrEmpty(identityUserId, nameof(identityUserId));
            Guard.Against.CepNuloOuVazio(cep);
            Guard.Against.InvalidFormat(cep, nameof(cep), @"^\d{8}$");
            Guard.Against.NullOrEmpty(numero, nameof(numero));

            var carrinho = await _carrinhoService.ObterPorIdentityUserId(identityUserId);

            Guard.Against.CarrinhoNulo(identityUserId, carrinho);
            Guard.Against.CarrinhoVazio(identityUserId, carrinho);
            Guard.Against.ProdutoForaDeEstoqueCheckout(carrinho);

            var pedido = await ObterPedidoPorUsuarioECep(identityUserId, carrinho, cep, numero, cancellationToken);

            var result = await _pedidoRepository.AddAsync(pedido, cancellationToken);

            var produtosUpdate = carrinho.Itens.Select(i => {
                i.Produto.SetQuantidadeEmEstoque(i.Produto.QuantidadeEmEstoque - i.Quantidade);
                return i.Produto;
            }).ToList();

            _produtoRepository.UpdateRange(produtosUpdate);

            await _carrinhoService.RemoveCarrinhoAsync(carrinho);

            return result;
        }

        public async Task<Pedido> ResumoPedido(string identityUserId, string cep, string numero = null,
            CancellationToken cancellationToken = default)
        {
            Guard.Against.NullOrEmpty(identityUserId, nameof(identityUserId));
            Guard.Against.CepNuloOuVazio(cep);
            Guard.Against.InvalidFormat(cep, nameof(cep), @"^\d{8}$");

            var carrinho = await _carrinhoService.ObterPorIdentityUserId(identityUserId);

            Pedido pedido = await ObterPedidoPorUsuarioECep(identityUserId, carrinho, cep, numero, cancellationToken);

            return pedido;
        }

        private async Task<Pedido> ObterPedidoPorUsuarioECep(string identityUserId,
            Carrinho carrinho, string cep, string numero = null, CancellationToken cancellationToken = default)
        {
            var itens = carrinho
                .Itens
                .Select(i => new PedidoItem(0,
                    i.Produto.Id,
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

        public async Task<Pedido> CancelaPedido(Pedido pedido,
            CancellationToken cancellationToken = default)
        {
            Guard.Against.Null(pedido, nameof(pedido));
            Guard.Against.PedidoJaPagoEProcessado(pedido.Status);

            await AtualizaStatus(pedido, StatusPedido.Cancelado, cancellationToken);

            var produtosSpec = new ProdutoFilterSpecification(pedido.Itens.Select(i => i.ProdutoId));
            var produtoList = await _produtoRepository.ListAsync(produtosSpec);

            produtoList = produtoList.Select(p => {
                var quantidadeItemPedidoCancelado = pedido
                    .Itens
                    .Where(i => i.ProdutoId == p.Id)
                    .FirstOrDefault()
                    .Quantidade;

                p.SetQuantidadeEmEstoque(p.QuantidadeEmEstoque + quantidadeItemPedidoCancelado);

                return p;
            }).ToList();

            _produtoRepository.UpdateRange(produtoList);

            return pedido;
        }

        private async Task AtualizaStatus(Pedido pedido, StatusPedido status, 
            CancellationToken cancellationToken = default)
        {
            pedido.SetStatus(status);

            await _pedidoRepository.UpdateAsync(pedido, cancellationToken);
        }

    }
}
