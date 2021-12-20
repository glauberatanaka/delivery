using Ardalis.GuardClauses;
using Delivery.Core.Entities.CarrinhoAggregate;
using Delivery.Core.Exceptions;

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
        public static void CepNuloOuVazio(this IGuardClause guardClause, string cep)
        {
            if (string.IsNullOrEmpty(cep))
            {
                throw new CepNuloOuVazioException();
            }
        }
    }
}
