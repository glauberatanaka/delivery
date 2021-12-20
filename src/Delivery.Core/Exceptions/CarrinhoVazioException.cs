using Delivery.Core.Entities.CarrinhoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Core.Exceptions
{
    public class CarrinhoVazioException : Exception
    {
        public CarrinhoVazioException(string identityUserId) : base($"Carrinho do usuário {identityUserId} está vazio.")
        {

        }
    }
}
