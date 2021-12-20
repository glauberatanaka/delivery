using System;

namespace Delivery.Core.Exceptions
{
    public class CepNuloOuVazioException : Exception
    {
        public CepNuloOuVazioException() : base("Cep não pode ser nulo ou vazio")
        {
        }
    }
}
