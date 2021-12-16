using FluentValidation;

namespace Delivery.Api.Endpoints.ProdutoEndpoints
{
    public class PatchValidator : AbstractValidator<PatchRequest>
    {
        public PatchValidator()
        {
            RuleFor(x => x.ProdutoId).NotEmpty().WithMessage("ProdutoId é obrigatório.");
        }
    }
}
