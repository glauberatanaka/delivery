using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.PedidoEndpoints
{
    public class CreateValidator : AbstractValidator<CreateRequest>
    {
        public CreateValidator()
        {
            RuleFor(x => x.Cep)
                .NotEmpty()
                .WithMessage("Cep obrigatório.");

            RuleFor(x => x.Numero)
                .NotEmpty()
                .WithMessage("Número obrigatório.");
        }
    }
}
