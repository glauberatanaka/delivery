using FluentValidation;

namespace Delivery.Api.Endpoints.PedidoEndpoints
{
    public class ResumoRequestValidator : AbstractValidator<ResumoRequest>
    {
        public ResumoRequestValidator()
        {
            RuleFor(x => x.Cep)
                .NotEmpty()
                .WithMessage("Cep obrigatório.");

            RuleFor(x => x.Cep)
                .Matches(@"\d{8}")
                .WithMessage("Formato inválido.");
        }
    }
}
