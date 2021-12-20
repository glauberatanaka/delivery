using System;

namespace Delivery.Core.Exceptions
{
    public class CarrinhoNuloException : Exception
    {
        public CarrinhoNuloException(int carrinhoId) : base($"Nenhum carrinho encontrado com o Id {carrinhoId}")
        {

        }
        public CarrinhoNuloException(string identityUserId) : base($"Nenhum carrinho encontrado para o usuário Id {identityUserId}")
        {

        }
    }
}
