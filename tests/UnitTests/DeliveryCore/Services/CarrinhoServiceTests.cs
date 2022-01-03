using AutoFixture;
using Delivery.Core.Entities.CarrinhoAggregate;
using Delivery.Core.Entities.ProdutoAggregate;
using Delivery.Core.Interfaces;
using Delivery.Core.Services;
using Delivery.Core.Specifications;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.DeliveryCore.Services
{
    public class CarrinhoServiceTests
    {
        private readonly CarrinhoService _carrinhoService;
        private readonly IRepository<Carrinho> _carrinhoRepository = Substitute.For<IRepository<Carrinho>>();
        private readonly IProdutoRepository _produtoRepository = Substitute.For<IProdutoRepository>();
        private readonly string _userIdentityId = "Test userIdentityId";
        private readonly IFixture _fixture = new Fixture();


        public CarrinhoServiceTests()
        {
            _carrinhoService = new CarrinhoService(_carrinhoRepository, _produtoRepository);
        }

        [Fact]
        public async Task AdicionaItemAoCarrinhoAsync_DeveAdicionarItemAoCarrinho_QuandoParametrosSaoValidos()
        {
            //Arrange
            const int quantidade = 10;
            var carrinho = _fixture.Create<Carrinho>();
            var produto = _fixture.Create<Produto>();

            _carrinhoRepository.GetBySpecAsync(Arg.Any<CarrinhoComItensEProdutosSpecification>(), default).Returns(carrinho);
            _produtoRepository.GetByIdAsync(produto.Id, default).Returns(produto);

            //Act
            await _carrinhoService.AdicionaItemAoCarrinhoAsync(_userIdentityId, produto.Id, quantidade, default);

            //Assert
            await _carrinhoRepository.DidNotReceive().AddAsync(Arg.Any<Carrinho>(), default);
            await _carrinhoRepository.Received(1).UpdateAsync(Arg.Any<Carrinho>());
        }
    }
}
