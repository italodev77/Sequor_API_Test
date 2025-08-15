using FluentValidation;
using Sequor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequor.Domain.EntitiesValidator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("Formato de email inválido.")
                .MaximumLength(100);

            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(50);

            RuleFor(u => u.InitialDate)
                .LessThanOrEqualTo(u => u.EndDate)
                .WithMessage("A data inicial não pode ser maior que a data final.");
        }
    }
}
