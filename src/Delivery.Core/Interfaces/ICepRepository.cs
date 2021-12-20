using Delivery.Shared.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Core.Interfaces
{
    public interface ICepRepository
    {
        Task<EnderecoCepModel> GetPorCepAsync(string cep, CancellationToken cancellationToken);
    }
}
