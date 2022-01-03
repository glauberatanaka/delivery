using Ardalis.GuardClauses;
using Delivery.Core.Entities.CarrinhoAggregate;
using Delivery.Core.Exceptions;
using Delivery.Shared.Enums;

namespace Delivery.Core.Extensions
{
    public static class GuardExtensions
    {
        public static void CarrinhoNulo(this IGuardClause guardClause, int carrinhoId, Carrinho carrinho)
        {
            if (carrinho is null)
            {
                throw new CarrinhoNuloException(carrinhoId);
            }
        }

        public static void CarrinhoNulo(this IGuardClause guardClause, string identityUserId, Carrinho carrinho)
        {
            if (carrinho is null)
            {
                throw new CarrinhoNuloException(identityUserId);
            }
        }

        public static void CarrinhoVazio(this IGuardClause guardClause, string identityUserId, Carrinho carrinho)
        {
            if (carrinho is null)
            {
                throw new CarrinhoVazioException(identityUserId);
            }
        }

        public static void CepNuloOuVazio(this IGuardClause guardClause, string cep)
        {
            if (string.IsNullOrEmpty(cep))
            {
                throw new CepNuloOuVazioException();
            }
        }

        public static void PedidoJaPagoEProcessado(this IGuardClause guardClause, StatusPedido status)
        {
            if (status is not StatusPedido.EmProcessamento and not StatusPedido.AguardandoPagamento)
            {
                throw new PedidoJaProcessadoException();
            }
        }

        public static void ProdutoForaDeEstoque(this IGuardClause guardClause,
             int quantidadeEmEstoque, int quantidadeAVerificar)
        {
            if (quantidadeEmEstoque < quantidadeAVerificar)
            {
                throw new ProdutoSemEstoqueException();
            }
        }

        public static void ProdutoForaDeEstoqueCheckout(this IGuardClause guardClause,
             Carrinho carrinho)
        {
            string produtosString = "";
            foreach (var item in carrinho.Itens)
            {
                if (item.Quantidade > item.Produto.QuantidadeEmEstoque)
                {
                    produtosString += item.Produto.Nome + "; ";
                }
            }
            if (!string.IsNullOrEmpty(produtosString))
            {
                throw new ProdutoSemEstoqueException(produtosString);
            }
        }


    }
}
