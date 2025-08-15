using FluentValidation;
using Sequor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.EntitiesValidator
{
    public class ProductMaterialValidator : AbstractValidator<ProductMaterial>
    {
        public ProductMaterialValidator()
        {
            RuleFor(pm => pm.ProductCode)
                .NotEmpty().WithMessage("O código do produto é obrigatório.")
                .MaximumLength(50);

            RuleFor(pm => pm.MaterialCode)
                .NotEmpty().WithMessage("O código do material é obrigatório.")
                .MaximumLength(50);
        }
    }
}
