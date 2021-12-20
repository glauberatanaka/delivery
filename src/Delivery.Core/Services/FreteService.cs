using Ardalis.GuardClauses;
using Delivery.Core.Constants;
using Delivery.Core.Entities.PedidoAggregate;
using Delivery.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Core.Services
{
    class FreteService : IFreteService
    {
        private readonly ICepRepository _cepRepository;

        public FreteService(ICepRepository cepRepository)
        {
            _cepRepository = cepRepository;
        }

        public async Task<decimal> CalculaFrete(string cep, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrEmpty(cep, nameof(cep));
            var cepEndereco = await _cepRepository.GetPorCepAsync(cep, cancellationToken);

            decimal valorFrete = cepEndereco switch
            {
                { Localidade: FreteConstants.LOCALIDADE_ORIGEM, Uf: FreteConstants.UF_ORIGEM } => 10,
                { Localidade: _, Uf: FreteConstants.UF_ORIGEM } => 20,
                _ => 40
            };
            return valorFrete;
        }

        public async Task<PedidoEndereco> EnderecoPorCep(string cep,
            string numero = null,
            string complemento = null,
            CancellationToken cancellationToken = default)
        {
            Guard.Against.NullOrEmpty(cep, nameof(cep));
            var cepEndereco = await _cepRepository.GetPorCepAsync(cep, cancellationToken);

            var response = new PedidoEndereco(cepEndereco.Cep, cepEndereco.Uf, cepEndereco.Localidade, numero, complemento);

            return response;
        }

    }
}
