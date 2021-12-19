using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.Api.Endpoints.CarrinhoEndpoints
{
    public class AddItemCarrinhoValidator : AbstractValidator<AddItemCarrinhoRequest>
    {
        public AddItemCarrinhoValidator()
        {
            RuleFor(r => r.ProdutoId)
                .NotEmpty()
                .WithMessage("ProdutoId obrigatório");

            RuleFor(r => r.Quantidade)
                .NotEmpty()
                .WithMessage("Quantidade obrigatória");
        }
    }
}
