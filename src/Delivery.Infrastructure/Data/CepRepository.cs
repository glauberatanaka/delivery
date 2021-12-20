using Delivery.Core.Interfaces;
using Delivery.Shared.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Infrastructure.Data
{
    public class CepRepository : ICepRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CepRepository(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<EnderecoCepModel> GetPorCepAsync(string cep, CancellationToken cancellationToken = default)
        {
            var client = _httpClientFactory.CreateClient("CepClient");

            var response = await client.GetStringAsync($"{cep}/json", cancellationToken);

            var endereco = JsonConvert.DeserializeObject<EnderecoCepModel>(response);

            return endereco;
        }
    }
}
