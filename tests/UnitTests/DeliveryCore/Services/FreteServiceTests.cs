using AutoFixture;
using Delivery.Core.Constants;
using Delivery.Core.Interfaces;
using Delivery.Core.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.DeliveryCore.Services
{
    public class FreteServiceTests
    {
        private readonly ICepRepository _cepRepository = Substitute.For<ICepRepository>();
        private readonly FreteService _freteService;
        private readonly IFixture _fixture = new Fixture();

        public FreteServiceTests()
        {
            _freteService = new FreteService(_cepRepository);
        }

        [Fact]
        public void CalculaFrete_DeveSer10_QuandoMesmaCidade()
        {
            //Arrange
            const string localidade_origem = FreteConstants.LOCALIDADE_ORIGEM;
            const string uf_origem = FreteConstants.UF_ORIGEM;

            //Act
            var result = _freteService.CalculaFrete(localidade_origem, uf_origem);

            //Assert
            Assert.Equal(10m, result);
        }
        [Fact]
        public void CalculaFrete_DeveSer20_QuandoMesmaUfCidadeDiferente()
        {
            //Arrange
            const string localidade_origem = "Petrópolis";
            const string uf_origem = FreteConstants.UF_ORIGEM;

            //Act
            var result = _freteService.CalculaFrete(localidade_origem, uf_origem);

            //Assert
            Assert.Equal(20m, result);
        }
        [Fact]
        public void CalculaFrete_DeveSer40_QuandoMesmaCidade()
        {
            //Arrange
            const string localidade_origem = "Cuiabá";
            const string uf_origem = "MT";

            //Act
            var result = _freteService.CalculaFrete(localidade_origem, uf_origem);

            //Assert
            Assert.Equal(40m, result);
        }
    }
}
