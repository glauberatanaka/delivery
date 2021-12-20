using Delivery.Core.Entities.PedidoAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface IFreteService
    {
        Task<decimal> CalculaFrete(string cep, CancellationToken cancellationToken = default);
        Task<PedidoEndereco> EnderecoPorCep(string cep,
            string numero = null,
            string complemento = null,
            CancellationToken cancellationToken = default);

        decimal CalculaFrete(string localidade, string uf);
    }
}
