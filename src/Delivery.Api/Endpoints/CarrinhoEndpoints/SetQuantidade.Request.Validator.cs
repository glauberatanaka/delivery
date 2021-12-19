using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.CarrinhoEndpoints
{
    public class DefineQuantidadeValidator : AbstractValidator<SetQuantidadeRequest>
    {
        public DefineQuantidadeValidator()
        {
            RuleFor(x => x.ProdutoId)
                .NotEmpty()
                .WithMessage("Carrinho id obrigatório.");

            RuleFor(x => x.Quantidade)
                .NotNull()
                .WithMessage("Carrinho id obrigatório.");
        }
    }
}
