using System;

namespace Delivery.Core.Exceptions
{
    public class CarrinhoNuloException : Exception
    {
        public CarrinhoNuloException(int carrinhoId) : base($"Nenhum carrinho encontrado com o Id {carrinhoId}")
        {

        }
    }
}
