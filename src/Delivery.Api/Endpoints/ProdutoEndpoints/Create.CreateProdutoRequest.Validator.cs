using FluentValidation;

namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class CreateProdutoRequestValidator : AbstractValidator<CreateProdutoRequest>
    {
        public CreateProdutoRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty();

            RuleFor(x => x.Preco)
                .NotEmpty()
                .WithMessage("'Preco' deve ser positivo e maior que zero.");

            RuleFor(x => x.QuantidadeEmEstoque)
                .NotNull();
        }
    }
}
