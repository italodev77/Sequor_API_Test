using FluentValidation;
using Sequor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.EntitiesValidator
{
    public class MaterialValidator : AbstractValidator<Material>
    {
        public MaterialValidator()
        {
            RuleFor(m => m.MaterialCode)
                .NotEmpty().WithMessage("O código do material é obrigatório.")
                .MaximumLength(50);

            RuleFor(m => m.MaterialDescription)
                .NotEmpty().WithMessage("A descrição do material é obrigatória.")
                .MaximumLength(500);
        }
    }
}
