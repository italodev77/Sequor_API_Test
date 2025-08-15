using FluentValidation;
using Sequor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.EntitiesValidator
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductCode)
                .NotEmpty().WithMessage("O código do produto é obrigatório.")
                .MaximumLength(50);

            RuleFor(p => p.ProductDescription)
                .NotEmpty().WithMessage("A descrição do produto é obrigatória.")
                .MaximumLength(50);

            RuleFor(p => p.CycleTime)
                .GreaterThanOrEqualTo(0).WithMessage("O tempo de ciclo deve ser maior ou igual a zero.");
        }
    }
}
