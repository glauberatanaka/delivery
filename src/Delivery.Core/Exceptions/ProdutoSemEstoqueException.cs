using System;

namespace Delivery.Core.Exceptions
{
    public class ProdutoSemEstoqueException : Exception
    {
        public ProdutoSemEstoqueException() : base($"Produto fora de estoque")
        {
        }
        public ProdutoSemEstoqueException(string produtos) : base($"Produto(s) fora de estoque: {produtos}")
        {
        }
    }
}
