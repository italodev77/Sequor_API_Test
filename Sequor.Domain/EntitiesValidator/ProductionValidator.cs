using FluentValidation;
using Sequor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.EntitiesValidator
{
    public class ProductionValidator : AbstractValidator<Production>
    {
        public ProductionValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .MaximumLength(100)
                .EmailAddress();

            RuleFor(p => p.OrderId)
                .NotEmpty().WithMessage("O código da ordem é obrigatório.")
                .MaximumLength(50);

            RuleFor(p => p.MaterialCode)
                .NotEmpty().WithMessage("O código do material é obrigatório.")
                .MaximumLength(50);

            RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

            RuleFor(p => p.CycleTime)
                .GreaterThanOrEqualTo(0).WithMessage("O tempo de ciclo deve ser maior ou igual a zero.");
        }
    }
}
