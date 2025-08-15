using FluentValidation;
using Sequor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.EntitiesValidator
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.OrderId)
                .NotEmpty().WithMessage("O código da ordem é obrigatório.")
                .MaximumLength(50);

            RuleFor(o => o.ProductCode)
                .NotEmpty().WithMessage("O código do produto é obrigatório.")
                .MaximumLength(50);

            RuleFor(o => o.Quantity)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
        }
    }
}
